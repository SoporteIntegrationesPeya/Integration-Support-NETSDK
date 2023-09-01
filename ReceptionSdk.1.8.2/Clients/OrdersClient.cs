///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using ReceptionSdk.Exceptions;
using ReceptionSdk.Http;
using ReceptionSdk.Models;
using ReceptionSdk.Helpers;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Dynamic;
using ReceptionSdk.Utils;
using Message = Amazon.SQS.Model.Message;
using Newtonsoft.Json;
using RestSharp.Deserializers;

namespace ReceptionSdk.Clients
{
    /// <summary>
    /// A client for Orders API.
    /// </summary>
    public class OrdersClient
    {
        private long timestamp = 0L;
        private long Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }
        private const int WAIT_TIME_SECONDS = 20;
        private const int POLL_TIME_SECONDS = 10;
        private const int MAX_RETRIES = 5;
        private const int MAX_NUMBER_OF_MESSAGES = 10;
        private const int CONFIRM = 2;
        private const int REJECT = 3;

        private const string HEADER_PARTNER = "Peya-Partner-Id";
        private const string HEADER_VERSION = "Peya-Sdk-Version";
        private const string HEADER_RS = "Peya-Reception-System-Code";

        private ApiConnection Connection { get; set; }

        /// <summary>
        /// Client for the DeliveryTimes API
        /// </summary>
        public DeliveryTimesClient DeliveryTime { get; private set; }

        /// <summary>
        /// Client for the RejectMessages API
        /// </summary>
        public RejectMessagesClient RejectMessage { get; private set; }

        /// <summary>
        /// Instantiate a new Orders API client.
        /// </summary>
        /// <param name="connection">an API connection</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        public OrdersClient(ApiConnection connection)
        {
            Ensure.ArgumentNotNull(connection, "connection");
 
            Connection = connection;
            DeliveryTime = new DeliveryTimesClient(Connection);
            RejectMessage = new RejectMessagesClient(Connection);
        }

        /// <summary>
        /// Returns an operational order by it's id
        /// </summary>
        /// <param name="id">the order identification number</param>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>the order with the specified id</returns>
        public Order Get(long id)
        {
            try
            {
                Request request = new Request
                {
                    Endpoint = new Uri($"orders/{id}", UriKind.Relative)
                };
                Response response = Connection.Get<Order>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return response.Body;
                }
                else
                {
                    throw new ApiException(response);
                }
            }
            catch (ApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        /// <summary>
        /// Delegate to be executed when a new order or order update is received
        /// </summary>
        /// <param name="order">the received order to be processed</param>
        /// <returns><c>true</c> if the order processed correctly</returns>
        public delegate bool OnReceivedOrder(Order order);

        /// <summary>
        /// Delegate to be executed when a error occurred receving a new order
        /// </summary>
        /// <param name="ex">the exception thrown</param>
        public delegate void OnError(ApiException ex);

        /// <summary>
        /// Listen for new orders. This will return new and orders updates
        /// </summary>
        /// <param name="onSuccess">delegate to be called when a new order is received</param>
        /// <param name="onError">delegate to be called when a error has occurred</param>
        public void GetAll(OnReceivedOrder onSuccess, OnError onError)
        {
            ApiCredentials credentials = Connection.Credentials as ApiCredentials;
            Connection.DoAuthenticate();
            if (credentials.PushAvailable())
            {
                PushOrders(onSuccess, onError);
            }
            else
            {
                PollOrders(onSuccess, onError);
            }
        }

        /// <summary>
        /// Get all the operational orders by state inside de pagination defined
        /// </summary>
        /// <param name="state">state of the orders</param>
        /// <see cref="OrderState"/>
        /// <param name="options">the pagination configuration</param>
        /// <see cref="PaginationOptions"/>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>a list of orders with the specified state, it may be an empty list</returns>
        public IList<Order> GetAll(string state, PaginationOptions options)
        {
            ApiCredentials credentials = Connection.Credentials as ApiCredentials;
            Connection.DoAuthenticate();
            if (state == OrderState.PENDING && credentials.PushAvailable())
            {
                throw new ApiException("Cannot get PENDING orders from this method using this kind of credentials");
            }
            return LoadOrders(state, options);
        }

        /// <summary>
        /// Confirms a pending order. This method must be called when the restaurant accepts the order
        /// </summary>
        /// <param name="orderId">the order id to be confirmed</param>
        /// <param name="deliveryTimeId">the delivery time id selected</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns><c>true</c> if the order was confirmed</returns>
        public bool Confirm(long orderId, long deliveryTimeId)
        {
            Ensure.GreaterThanZero(orderId, "orderId");
            Ensure.GreaterThanZero(deliveryTimeId, "deliveryTimeId");

            return Update(orderId, CONFIRM, deliveryTimeId, null, false, null, null, null);
        }

        /// <summary>
        /// Confirm a pending order. This method must be called when the restaurant accepts the order
        /// </summary>
        /// <param name="order">the order to be confirmed</param>
        /// <param name="deliveryTime">the delivery time selected</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns><c>true</c> if the order was confirmed</returns>
        /// <seealso cref="Order"/>
        /// <seealso cref="DeliveryTime"/>
        public bool Confirm(Order order, DeliveryTime deliveryTime)
        {
            Ensure.ArgumentNotNull(order, "order");
            Ensure.ArgumentNotNull(deliveryTime, "deliveryTime");
            Ensure.GreaterThanZero(order.Id, "order.Id");
            Ensure.GreaterThanZero(deliveryTime.Id, "deliveryTime.Id");

            if (order.Logistics == true || order.PreOrder == true)
            {
                throw new ApiException("Cannot confirm order from this method, this method is not for anticipated or logistics orders");
            }

            return Update(order.Id, CONFIRM, deliveryTime.Id, null, false, null, null, null);
        }

        /// <summary>
        /// Confirms a pending order anticipated or with logistics. This method must be called when the restaurant accepts the order
        /// </summary>
        /// <param name="orderId">the order id to be confirmed</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns><c>true</c> if the order was confirmed</returns>
        public bool Confirm(long orderId)
        {
            Ensure.GreaterThanZero(orderId, "orderId");

            return Update(orderId, CONFIRM, null, null, false, null, null, null);
        }

        /// <summary>
        /// Confirms a pending order anticipated or with logistics. This method must be called when the restaurant accepts the order
        /// </summary>
        /// <param name="order">the order to be confirmed</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns><c>true</c> if the order was confirmed</returns>
        /// <seealso cref="Order"/>
        /// <seealso cref="DeliveryTime"/>
        public bool Confirm(Order order)
        {
            Ensure.ArgumentNotNull(order, "order");
            Ensure.GreaterThanZero(order.Id, "order.Id");

            if (order.Logistics == false && order.PreOrder == false)
            {
                throw new ApiException("Cannot confirm order from this method, this method is for anticipated or logistics orders");
            }

            return Update(order.Id, CONFIRM, null, null, false, null, null, null);
        }

        /// <summary>
        /// Dispatch an order. This method must be called when the restaurant is ready to deliver the order
        /// </summary>
        /// <param name="orderId">the order id to be dispatched</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns><c>true</c> if the order was dispatched</returns>
        public bool Dispatch(long orderId)
        {
            Ensure.GreaterThanZero(orderId, "orderId");

            return Update(orderId, null, null, null, true, null, null, null);
        }

        /// <summary>
        /// Dispatch an order. This method must be called when the restaurant is ready to deliver the order
        /// </summary>
        /// <param name="order">the order to be dispatched</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns><c>true</c> if the order was dispatched</returns>
        /// <seealso cref="Order"/>
        public bool Dispatch(Order order)
        {
            Ensure.ArgumentNotNull(order, "order");
            Ensure.GreaterThanZero(order.Id, "order.Id");

            return Update(order.Id, null, null, null, true, null, null, null);
        }

        /// <summary>
        /// Rejects a pending order. This method must be called when the restaurant cannot accept the order
        /// </summary>
        /// <param name="orderId">the order id to be rejected</param>
        /// <param name="rejectMessageId">the reject message selected</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns><c>true</c>if the order was rejected</returns>
        public bool Reject(long orderId, long rejectMessageId)
        {
            Ensure.GreaterThanZero(orderId, "orderId");
            Ensure.GreaterThanZero(rejectMessageId, "rejectMessageId");

            return Update(orderId, REJECT, null, rejectMessageId, false, null, null, null);
        }

        /// <summary>
        /// Rejects a pending order with a rejection note. This method must be called when the restaurant cannot accept the order
        /// </summary>
        /// <param name="orderId">the order id to be rejected</param>
        /// <param name="rejectMessageId">the reject message selected</param>
        /// <param name="rejectionNote">the rejection note</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns><c>true</c>if the order was rejected</returns>
        public bool Reject(long orderId, long rejectMessageId, string rejectionNote)
        {
            Ensure.GreaterThanZero(orderId, "orderId");
            Ensure.GreaterThanZero(rejectMessageId, "rejectMessageId");
            Ensure.ArgumentNotNullOrEmptyString(rejectionNote, "rejectionNote");

            return Update(orderId, REJECT, null, rejectMessageId, false, rejectionNote, null, null);
        }

        /// <summary>
        /// Rejects a pending order. This method must be called when the restaurant cannot accept the order
        /// </summary>
        /// <param name="order">the order to be rejected</param>
        /// <param name="rejectMessage">the reject message selected</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns><c>true</c> if the order was rejected</returns>
        /// <seealso cref="Order"/>
        /// <seealso cref="RejectMessage"/>
        public bool Reject(Order order, RejectMessage rejectMessage)
        {
            Ensure.ArgumentNotNull(order, "order");
            Ensure.ArgumentNotNull(rejectMessage, "rejectMessage");
            Ensure.GreaterThanZero(order.Id, "order.Id");
            Ensure.GreaterThanZero(rejectMessage.Id, "rejectMessage.Id");

            return Update(order.Id, REJECT, null, rejectMessage.Id, false, null, null, null);
        }

        /// <summary>
        /// Rejects a pending order with a rejection note. This method must be called when the restaurant cannot accept the order
        /// </summary>
        /// <param name="order">the order to be rejected</param>
        /// <param name="rejectMessage">the reject message selected</param>
        /// <param name="note">the rejection note</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns><c>true</c> if the order was rejected</returns>
        /// <seealso cref="Order"/>
        /// <seealso cref="RejectMessage"/>
        public bool Reject(Order order, RejectMessage rejectMessage, string rejectionNote)
        {
            Ensure.ArgumentNotNull(order, "order");
            Ensure.ArgumentNotNull(rejectMessage, "rejectMessage");
            Ensure.GreaterThanZero(order.Id, "order.Id");
            Ensure.GreaterThanZero(rejectMessage.Id, "rejectMessage.Id");
            Ensure.ArgumentNotNullOrEmptyString(rejectionNote, "rejectionNote");

            return Update(order.Id, REJECT, null, rejectMessage.Id, false, rejectionNote, null, null);
        }

        /// <summary>
        /// Bill an order. This method must be called after the order was confirmed
        /// </summary>
        /// <param name="orderId">the order id to be rejected</param>
        /// <param name="invoiceNumber">the order bill number</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns><c>true</c> if the order was billed</returns>
        public bool Bill(long orderId, string invoiceNumber)
        {
            Ensure.GreaterThanZero(orderId, "orderId");
            Ensure.ArgumentNotNullOrEmptyString(invoiceNumber, "invoiceNumber");
            
            return Update(orderId, null, null, null, false, null, invoiceNumber, null);
        }

        /// <summary>
        /// Bill an order. This method must be called after the order was confirmed
        /// </summary>
        /// <param name="orderId">the order id to be rejected</param>
        /// <param name="invoiceNumber">the order bill number</param>
        /// <param name="total">the new order total</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns><c>true</c> if the order was billed</returns>
        public bool Bill(long orderId, string invoiceNumber, double total)
        {
            Ensure.GreaterThanZero(orderId, "orderId");
            Ensure.ArgumentNotNullOrEmptyString(invoiceNumber, "invoiceNumber");
            Ensure.GreaterOrEqualsThanZero(total, "total");

            return Update(orderId, null, null, null, false, null, invoiceNumber, total);
        }

        /// <summary>
        /// Retrieves the OrderTracking given an orderId
        /// </summary>
        /// <param name="orderId">the order id to be tracked</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>the order tracking</returns>
        public OrderTracking Tracking(long orderId)
        {
            Ensure.GreaterThanZero(orderId, "orderId");
            try
            {
                Request request = new Request
                {
                    Endpoint = new Uri($"orders/{orderId}/tracking", UriKind.Relative)
                };
                Response response = Connection.Get<OrderTracking>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    OrderTracking orderTracking = response.Body;
                    return orderTracking;
                }
                else
                {
                    throw new ApiException(response);
                }
            }
            catch (ApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public IList<object> GetTaxes(string[] productIntegrationCodes, long restaurantId)
        {
            Ensure.GreaterThanZero(restaurantId, "restaurantId");

            Connection.DoAuthenticate();
            try
            {
                Request request = BuildRequestTaxes(productIntegrationCodes, restaurantId);
                dynamic headers = AddGeneralHeaders(restaurantId);
                request.Headers = headers;

                Response response = Connection.Post<GenericResponse<object>>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    object data = response.Body.data;
                    IList<object> ret = JsonConvert.DeserializeObject<List<object>>(data.ToString());
                    return ret;
                }
                else
                {
                    throw new ApiException(response);
                }
            }
            catch (ApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        private Request BuildRequestTaxes(string[] productIntegrationCodes, long restaurantId)
        {
            Request request = new Request
            {
                Endpoint = new Uri($"{Connection.ISMSUrl()}v1/product/taxes", UriKind.Absolute),
                Timeout = TimeSpan.FromMinutes(1),
                Body = TaxesPayload(productIntegrationCodes, restaurantId)
            };
            return request;
        }

        private dynamic AddGeneralHeaders(long restaurantId)
        {
            dynamic headers = new ExpandoObject();
            ((IDictionary<String, Object>)headers).Add(HEADER_PARTNER, restaurantId);
            ((IDictionary<String, Object>)headers).Add(HEADER_VERSION, EventsClient.Version);
            return headers;
        }

        private dynamic TaxesPayload(string[] productIntegrationCodes, long restaurantId)
        {
            dynamic body = productIntegrationCodes;
            return body;
        }

        private void PollOrders(OnReceivedOrder onSuccess, OnError onError)
        {
            Timer timer = new Timer(
                callback => {
                    try
                    {
                        IList<Order> orders = LoadOrders(OrderState.PENDING, PaginationOptions.Create());
                        foreach (Order order in orders)
                        {
                            onSuccess(order);
                        }
                    }
                    catch (ApiException ex)
                    {
                        onError(ex);
                    }
                    catch (Exception ex)
                    {
                        onError(new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR));
                    }
                }
            );
            timer.Change(TimeSpan.Zero, TimeSpan.FromSeconds(POLL_TIME_SECONDS));
        }

        private IList<Order> LoadOrders(string state, PaginationOptions options)
        {
            try
            {
                bool pendingState = state == OrderState.PENDING;
                Request request = new Request
                {
                    Endpoint = new Uri("orders", UriKind.Relative)
                };
                if (pendingState)
                {
                    request.Parameters.timestamp = Timestamp;
                }
                else
                {
                    request.Parameters.state = state;
                    request.Parameters.offset = options.Offset;
                    request.Parameters.limit = options.Limit;
                }
                Response response = Connection.Get<OrdersResponse>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    IList<Order> orders = response.Body.Orders;
                    if (pendingState && orders.Count > 0)
                    {
                        Timestamp = orders.Max(order => order != null ? order.Timestamp : Timestamp);
                    }
                    return orders;
                }
                else
                {
                    throw new ApiException(response);
                }
            }
            catch (ApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        private void PushOrders(OnReceivedOrder onSuccess, OnError onError)
        {
            Task.Factory
            .StartNew(() => LoadOrders(onSuccess, onError))
            .ContinueWith(task => {
                if (task.Exception != null)
                {
                    onError(new ApiException(task.Exception, ErrorCode.INTERNAL_SERVER_ERROR));
                }
            }, TaskContinuationOptions.OnlyOnFaulted);
        }

        private void LoadOrders(OnReceivedOrder onSuccess, OnError onError)
        {
            try
            {
                ApiCredentials apiCredentials = Connection.Credentials as ApiCredentials;
                AmazonSQSClient sqsClient = new AmazonSQSClient(
                    apiCredentials.OrderAccessKey, 
                    apiCredentials.OrderSecretKey, 
                    RegionEndpoint.GetBySystemName(apiCredentials.RegionEndpoint)
                );
                string queueUrl = sqsClient.GetQueueUrl(apiCredentials.QueueName).QueueUrl;
                ReceiveMessageRequest receiveMessageRequest = new ReceiveMessageRequest(queueUrl)
                {
                    AttributeNames = new List<string>() { "ApproximateReceiveCount" },
                    WaitTimeSeconds = WAIT_TIME_SECONDS,
                    MaxNumberOfMessages = MAX_NUMBER_OF_MESSAGES
                };
                ReceiveMessageResponse receiveMessageResponse = sqsClient.ReceiveMessage(receiveMessageRequest);
                ApiDeserializer deserializer = new ApiDeserializer();
                foreach (Message message in receiveMessageResponse.Messages)
                {
                    string receiveCount = message.Attributes?["ApproximateReceiveCount"];
                    int retries = receiveCount != null ? int.Parse(receiveCount) : 0;
                    if (retries >= MAX_RETRIES)
                    {
                        Task.Factory.FromAsync(
                            sqsClient.BeginDeleteMessage(new DeleteMessageRequest(queueUrl, message.ReceiptHandle), null, null),
                            sqsClient.EndDeleteMessage
                        );
                    }
                    else
                    {
                        Order order = deserializer.Deserialize<Order>(message.Body);
                        bool processed = onSuccess(order);
                        if (processed)
                        {
                            Task.Factory.FromAsync(
                                sqsClient.BeginDeleteMessage(new DeleteMessageRequest(queueUrl, message.ReceiptHandle), null, null),
                                sqsClient.EndDeleteMessage
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
            finally
            {
                PushOrders(onSuccess, onError);
            }
            return;
        }

        private bool Update(long id, int? state, long? deliveryTimeId, long? rejectMessageId, bool dispatched, String rejectionNote, String invoiceNumber, double? total)
        {
            try
            {
                Request request = new Request
                {
                    Endpoint = new Uri($"orders/{id}", UriKind.Relative)
                };
                dynamic body = new ExpandoObject();
                if (dispatched)
                {
                    body.dispatched = true;
                }
                else if (invoiceNumber != null)
                {
                    body.invoiceNumber = invoiceNumber;
                    if (total != null)
                    {
                        body.total = total;
                    }
                }
                else
                {
                    body.state = state;
                    if (state == CONFIRM && deliveryTimeId != null)
                    {
                        body.deliveryTimeId = deliveryTimeId;
                    }
                    else if (state == REJECT)
                    {
                        body.rejectMessageId = rejectMessageId;
                        body.notes = rejectionNote;
                    }
                }
                request.Body = body;
                Response response = Connection.Put<Dictionary<string, object>>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    throw new ApiException(response);
                }
            }
            catch (ApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }
        
        /// <summary>
        /// Reconcile order. 
        /// </summary>
        /// <param name="reconciliation">the data to reconcile the order</param>
        /// <param name="partnerId">the partner id</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>Returns if the order was reconciled</returns>
        public object Reconcile(Reconciliation reconciliation, long partnerId)
        {
            Ensure.GreaterThanZero(partnerId, "partnerId");
            Ensure.ArgumentNotNull(reconciliation, "reconciliation");
            
            try
            {
                var request = ReconciliationPayload(reconciliation, partnerId);
                Response response = Connection.Post<Dictionary<String, Object>>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return response.Body;
                }

                throw new ApiException(response);
            }
            catch (ApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }
        
        
        /// <summary>
        /// Reconcile order. 
        /// </summary>
        /// <param name="orderId">the order id</param>
        /// <param name="partnerId">the partner id</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>Returns if the checkout was success</returns>
        public object Checkout(long orderId, long partnerId)
        {
            Ensure.GreaterThanZero(orderId, "orderId");
            Ensure.GreaterThanZero(partnerId, "partnerId");
            
            try
            {
                Request request = new Request
                {
                    Endpoint = new Uri($"{Connection.IosUrl()}v1/orders/{orderId}/checkout")
                };
                dynamic headers = AddGeneralHeaders(partnerId);
                ((IDictionary<String, Object>)headers).Add(HEADER_RS, Connection.Credentials.ClientId);
                request.Headers = headers;
                
                Response response = Connection.Post<Dictionary<String, Object>>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return response.Body;
                }

                throw new ApiException(response);
            }
            catch (ApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        private Request ReconciliationPayload(Reconciliation reconciliation, long partnerId)
        {
            Request request = new Request
            {
                Endpoint = new Uri($"{Connection.IosUrl()}v1/orders/{reconciliation.OrderId}/reconcile")
            };
            dynamic headers = AddGeneralHeaders(partnerId);
            ((IDictionary<String, Object>)headers).Add(HEADER_RS, Connection.Credentials.ClientId);
            request.Headers = headers;
            request.Body = reconciliation;
            return request;
        }

        public object FoodIsReady (long orderId, long restaurantId)
        {
            try 
            {
                Ensure.GreaterThanZero(orderId, "orderId");
                Ensure.GreaterThanZero(restaurantId, "restaurantId");
                Request request = new Request
                {
                    Endpoint = new Uri($"{Connection.IosUrl()}v1/orders/{orderId}/preparation-completion")
                };
                dynamic headers = AddGeneralHeaders(restaurantId);
                ((IDictionary<String, Object>)headers).Add(HEADER_RS, Connection.Credentials.ClientId);
                request.Headers = headers;
                Response response = Connection.Post<Dictionary<string, object>>(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApiException(response);

                }
                return HttpUtils.ResponseBody(response);
            }
            catch (ApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

    }
    
    public class ReconciliationResponse
    {
        [DeserializeAs(Name = "response")] public String Response { get; }
    }
}
