///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using ReceptionSdk.Exceptions;
using ReceptionSdk.Helpers;
using ReceptionSdk.Http;
using ReceptionSdk.Models;
using System;
using System.Net;
using System.Dynamic;
using System.Collections.Generic;
using System.Linq;

namespace ReceptionSdk.Clients
{
    /// <summary>
    /// A client for Events API.
    /// </summary>
    public class EventsClient
    {
        private ApiConnection Connection { get; set; }

        public static string Version {
            get
            {
                return "1.8.2";
            }
            private set
            {
            }
        }

        /// <summary>
        /// Instantiate a new Events API client.
        /// </summary>
        /// <param name="connection">an API connection</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        public EventsClient(ApiConnection connection)
        {
            Ensure.ArgumentNotNull(connection, "connection");

            Connection = connection;
        }

        /// <summary>
        /// Register a new initialization event. 
        /// This event represents the startup of the reception system.
        /// </summary>
        /// <param name="version">all posible information about the device and reception app</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void Initialization(ExpandoObject version)
        {
            if (Connection.Credentials.CentralizedKeys())
            {
                throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
            }
            Ensure.ArgumentNotNull(version, "version");

            Initialization(version, null, null);
        }

        /// <summary>
        /// Register a new initialization event. 
        /// This event represents the startup of the reception system.
        /// </summary>
        /// <param name="version">all posible information about the device and reception app</param>
        /// <param name="restaurantCode">restaurant code (when using centralized keys)</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void Initialization(ExpandoObject version, string restaurantCode)
        {
            Ensure.ArgumentNotNull(version, "version");
            Ensure.ArgumentNotNullOrEmptyString(restaurantCode, "restaurantCode");

            Initialization(version, restaurantCode, null);
        }

        /// <summary>
        /// Register a new initialization event. 
        /// This event represents the startup of the reception system.
        /// </summary>
        /// <param name="version">all posible information about the device and reception app</param>
        /// <param name="restaurantId">restaurant id (when using centralized keys)</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void Initialization(ExpandoObject version, long restaurantId)
        {
            Ensure.ArgumentNotNull(version, "version");
            Ensure.GreaterThanZero(restaurantId, "restaurantId");

            Initialization(version, null, restaurantId);
        }

        /// <summary>
        /// Register a new heart beat event. 
        /// This event represents that the reception system it's alive and ready to receive orders
        /// </summary>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void HeartBeat()
        {
            if (Connection.Credentials.CentralizedKeys())
            {
                throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
            }

            HeartBeat(null, null);
        }

        /// <summary>
        /// Register a new heart beat event. 
        /// This event represents that the reception system it's alive and ready to receive orders
        /// </summary>
        /// <param name="restaurantCode">restaurant code (when using centralized keys)</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void HeartBeat(string restaurantCode)
        {
            Ensure.ArgumentNotNullOrEmptyString(restaurantCode, "restaurantCode");

            HeartBeat(restaurantCode, null);
        }

        /// <summary>
        /// Register a new heart beat event. 
        /// This event represents that the reception system it's alive and ready to receive orders
        /// </summary>
        /// <param name="restaurantId">restaurant id (when using centralized keys)</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void HeartBeat(long restaurantId)
        {
            Ensure.GreaterThanZero(restaurantId, "restaurantId");

            HeartBeat(null, restaurantId);
        }

        /// <summary>
        /// Register a reception event. 
        /// This event represents that the order has arrived
        /// </summary>
        /// <param name="order">order that has been received</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void Reception(Order order)
        {
            Ensure.ArgumentNotNull(order, "order");

            long orderId = order.Id;

            Ensure.GreaterThanZero(orderId, "order.Id");

            if (Connection.Credentials.CentralizedKeys())
            {
                if (order.Restaurant != null)
                {
                    long restaurantId = order.Restaurant.Id;
                    string restaurantCode = order.Restaurant.IntegrationCode;

                    if (restaurantId > 0)
                    {
                        Reception(orderId, restaurantId);
                    }
                    else if (!string.IsNullOrEmpty(restaurantCode))
                    {
                        Reception(orderId, restaurantCode);
                    }
                    else
                    {
                        throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
                    }
                }
                else
                {
                    throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
                }
            }
            else
            {
                Reception(order.Id);
            }
        }

        /// <summary>
        /// Register a reception event. 
        /// This event represents that the order has arrived
        /// </summary>
        /// <param name="orderId">order id that has been received</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void Reception(long orderId)
        {
            if (Connection.Credentials.CentralizedKeys())
            {
                throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
            }
            Ensure.GreaterThanZero(orderId, "orderId");

            Reception(orderId, null, null);
        }


        /// <summary>
        /// Register a reception event. 
        /// This event represents that the order has arrived
        /// </summary>
        /// <param name="orderId">order id that has been received</param>
        /// <param name="restaurantCode">restaurant code (when using centralized keys)</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void Reception(long orderId, string restaurantCode)
        {
            Ensure.GreaterThanZero(orderId, "orderId");
            Ensure.ArgumentNotNullOrEmptyString(restaurantCode, "restaurantCode");

            Reception(orderId, restaurantCode, null);
        }

        /// <summary>
        /// Register a reception event. 
        /// This event represents that the order has arrived
        /// </summary>
        /// <param name="orderId">order id that has been received</param>
        /// <param name="restaurantId">restaurant id (when using centralized keys)</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void Reception(long orderId, long restaurantId)
        {
            Ensure.GreaterThanZero(orderId, "orderId");
            Ensure.GreaterThanZero(restaurantId, "restaurantId");

            Reception(orderId, null, restaurantId);
        }

        /// <summary>
        /// Register a order acknowledgement event
        /// This event represents the order was seen by a restaurant operator
        /// </summary>
        /// <param name="order">order that has been received</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void Acknowledgement(Order order)
        {
            Ensure.ArgumentNotNull(order, "order");

            long orderId = order.Id;

            Ensure.GreaterThanZero(orderId, "order.Id");

            if (Connection.Credentials.CentralizedKeys())
            {
                if (order.Restaurant != null)
                {
                    long restaurantId = order.Restaurant.Id;
                    string restaurantCode = order.Restaurant.IntegrationCode;

                    if (restaurantId > 0)
                    {
                        Acknowledgement(orderId, restaurantId);
                    }
                    else if (!string.IsNullOrEmpty(restaurantCode))
                    {
                        Acknowledgement(orderId, restaurantCode);
                    }
                    else
                    {
                        throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
                    }
                }
                else
                {
                    throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
                }
            }
            else
            {
                Acknowledgement(order.Id);
            }
        }

        /// <summary>
        /// Register a order acknowledgement event. 
        /// This event represents the order was seen by a restaurant operator
        /// </summary>
        /// <param name="orderId">order id that has been received</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void Acknowledgement(long orderId)
        {
            if (Connection.Credentials.CentralizedKeys())
            {
                throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
            }
            Ensure.GreaterThanZero(orderId, "orderId");

            Acknowledgement(orderId, null, null);
        }


        /// <summary>
        /// Register a order acknowledgement event. 
        /// This event represents the order was seen by a restaurant operator
        /// </summary>
        /// <param name="orderId">order id that has been received</param>
        /// <param name="restaurantCode">restaurant code (when using centralized keys)</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void Acknowledgement(long orderId, string restaurantCode)
        {
            Ensure.GreaterThanZero(orderId, "orderId");
            Ensure.ArgumentNotNullOrEmptyString(restaurantCode, "restaurantCode");

            Acknowledgement(orderId, restaurantCode, null);
        }

        /// <summary>
        /// Register a order acknowledgement event. 
        /// This event represents the order was seen by a restaurant operator
        /// </summary>
        /// <param name="orderId">order id that has been received</param>
        /// <param name="restaurantId">restaurant id (when using centralized keys)</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void Acknowledgement(long orderId, long restaurantId)
        {
            Ensure.GreaterThanZero(orderId, "orderId");
            Ensure.GreaterThanZero(restaurantId, "restaurantId");

            Acknowledgement(orderId, null, restaurantId);
        }

        /// <summary>
        /// Register a order state change event
        /// This event represents that a state change of the order must be done
        /// </summary>
        /// <param name="order">order that has been received</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void StateChange(Order order)
        {
            Ensure.ArgumentNotNull(order, "order");

            long orderId = order.Id;
            string orderState = order.State;

            Ensure.GreaterThanZero(orderId, "order.Id");
            Ensure.ArgumentNotNullOrEmptyString(orderState, "order.State");

            if (Connection.Credentials.CentralizedKeys())
            {
                if (order.Restaurant != null)
                {
                    long restaurantId = order.Restaurant.Id;
                    string restaurantCode = order.Restaurant.IntegrationCode;

                    if (restaurantId > 0)
                    {
                        StateChange(orderId, orderState, restaurantId);
                    }
                    else if (!string.IsNullOrEmpty(restaurantCode))
                    {
                        StateChange(orderId, orderState, restaurantCode);
                    }
                    else
                    {
                        throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
                    }
                }
                else
                {
                    throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
                }
            }
            else
            {
                StateChange(order.Id, order.State, null, null);
            }
        }

        /// <summary>
        /// Register a order state change event
        /// This event represents that a state change of the order must be done
        /// </summary>
        /// <param name="orderId">order id that has been received</param>
        /// <param name="orderState">new state of the order</param>
        /// <see cref="OrderState"/>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void StateChange(long orderId, string orderState)
        {
            if (Connection.Credentials.CentralizedKeys())
            {
                throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
            }
            Ensure.GreaterThanZero(orderId, "orderId");
            Ensure.ArgumentNotNullOrEmptyString(orderState, "orderState");

            StateChange(orderId, orderState, null, null);
        }


        /// <summary>
        /// Register a order state change event
        /// This event represents that a state change of the order must be done
        /// </summary>
        /// <param name="orderId">order id that has been received</param>
        /// <param name="orderState">new state of the order</param>
        /// <param name="restaurantCode">restaurant code (when using centralized keys)</param>
        /// <see cref="OrderState"/>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void StateChange(long orderId, string orderState, string restaurantCode)
        {
            Ensure.GreaterThanZero(orderId, "orderId");
            Ensure.ArgumentNotNullOrEmptyString(orderState, "orderState");
            Ensure.ArgumentNotNullOrEmptyString(restaurantCode, "restaurantCode");

            StateChange(orderId, orderState, restaurantCode, null);
        }

        /// <summary>
        /// Register a order state change event
        /// This event represents that a state change of the order must be done
        /// </summary>
        /// <param name="orderId">order id that has been received</param>
        /// <param name="orderState">new state of the order</param>
        /// <param name="restaurantId">restaurant id (when using centralized keys)</param>
        /// <see cref="OrderState"/>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void StateChange(long orderId, string orderState, long restaurantId)
        {
            Ensure.GreaterThanZero(orderId, "orderId");
            Ensure.ArgumentNotNullOrEmptyString(orderState, "orderState");
            Ensure.GreaterThanZero(restaurantId, "restaurantId");

            StateChange(orderId, orderState, null, restaurantId);
        }

        /// <summary>
        /// Register a warning event.
        /// This event represents a warning, for example: low battery, lack of paper, etc.
        /// </summary>
        /// <param name="eventCode">code of the event</param>
        /// <param name="eventDescription">a brief description of the event</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void Warning(string eventCode, string eventDescription)
        {
            if (Connection.Credentials.CentralizedKeys())
            {
                throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
            }
            Ensure.ArgumentNotNullOrEmptyString(eventCode, "eventCode");
            Ensure.ArgumentNotNullOrEmptyString(eventDescription, "eventDescription");

            Warning(eventCode, eventDescription, null, null);
        }

        /// <summary>
        /// Register a warning event.
        /// This event represents a warning, for example: low battery, lack of paper, etc.
        /// </summary>
        /// <param name="eventCode">code of the event</param>
        /// <param name="eventDescription">a brief description of the event</param>
        /// <param name="restaurantCode">restaurant code (when using centralized keys)</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void Warning(string eventCode, string eventDescription, string restaurantCode)
        {
            Ensure.ArgumentNotNullOrEmptyString(eventCode, "eventCode");
            Ensure.ArgumentNotNullOrEmptyString(eventDescription, "eventDescription");
            Ensure.ArgumentNotNullOrEmptyString(restaurantCode, "restaurantCode");

            Warning(eventCode, eventDescription, restaurantCode, null);
        }

        /// <summary>
        /// Register a warning event.
        /// This event represents a warning, for example: low battery, lack of paper, etc.
        /// </summary>
        /// <param name="eventCode">code of the event</param>
        /// <param name="eventDescription">a brief description of the event</param>
        /// <param name="restaurantId">restaurant id (when using centralized keys)</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void Warning(string eventCode, string eventDescription, long restaurantId)
        {
            Ensure.ArgumentNotNullOrEmptyString(eventCode, "eventCode");
            Ensure.ArgumentNotNullOrEmptyString(eventDescription, "eventDescription");
            Ensure.GreaterThanZero(restaurantId, "restaurantId");

            Warning(eventCode, eventDescription, null, restaurantId);
        }

        /// <summary>
        /// Register a error event.
        /// This event represents a error, for example: missing product code, can't confirm order, error processing orders
        /// </summary>
        /// <param name="eventCode">code of the event</param>
        /// <param name="eventDescription">a brief description of the event</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void Error(string eventCode, string eventDescription)
        {
            if (Connection.Credentials.CentralizedKeys())
            {
                throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
            }
            Ensure.ArgumentNotNullOrEmptyString(eventCode, "eventCode");
            Ensure.ArgumentNotNullOrEmptyString(eventDescription, "eventDescription");

            Error(eventCode, eventDescription, null, null);
        }

        /// <summary>
        /// Register a error event.
        /// This event represents a error, for example: missing product code, can't confirm order, error processing orders
        /// </summary>
        /// <param name="eventCode">code of the event</param>
        /// <param name="eventDescription">a brief description of the event</param>
        /// <param name="restaurantCode">restaurant code (when using centralized keys)</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void Error(string eventCode, string eventDescription, string restaurantCode)
        {
            Ensure.ArgumentNotNullOrEmptyString(eventCode, "eventCode");
            Ensure.ArgumentNotNullOrEmptyString(eventDescription, "eventDescription");
            Ensure.ArgumentNotNullOrEmptyString(restaurantCode, "restaurantCode");

            Error(eventCode, eventDescription, restaurantCode, null);
        }

        /// <summary>
        /// Register a error event.
        /// This event represents a error, for example: missing product code, can't confirm order, error processing orders
        /// </summary>
        /// <param name="eventCode">code of the event</param>
        /// <param name="eventDescription">a brief description of the event</param>
        /// <param name="restaurantId">restaurant id (when using centralized keys)</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void Error(string eventCode, string eventDescription, long restaurantId)
        {
            Ensure.ArgumentNotNullOrEmptyString(eventCode, "eventCode");
            Ensure.ArgumentNotNullOrEmptyString(eventDescription, "eventDescription");
            Ensure.GreaterThanZero(restaurantId, "restaurantId");

            Error(eventCode, eventDescription, null, restaurantId);
        }

        private void Initialization(ExpandoObject version, string restaurantCode, long? restaurantId)
        {
            ((dynamic)version).sdk = $".Net-{Version}";
            dynamic data = new ExpandoObject();
            data.action = EventType.INITIALIZATION;
            data.version = string.Join("|", version.Select(attribute => $"{attribute.Key}={attribute.Value}").ToArray());
            data.restaurant = new ExpandoObject();
            if (restaurantCode != null)
            {
                data.restaurant.code = restaurantCode.Trim();
            }
            if (restaurantId != null)
            {
                data.restaurant.id = restaurantId;
            }

            Event(data);
        }

        private void HeartBeat(string restaurantCode, long? restaurantId)
        {
            dynamic data = new ExpandoObject();
            data.action = EventType.HEART_BEAT;
            data.restaurant = new ExpandoObject();
            if (restaurantCode != null)
            {
                data.restaurant.code = restaurantCode.Trim();
            }
            if (restaurantId != null)
            {
                data.restaurant.id = restaurantId;
            }

            Event(data);
        }

        private void Reception(long orderId, string restaurantCode, long? restaurantId)
        {
            dynamic data = new ExpandoObject();
            data.action = EventType.RECEPTION;
            data.order = orderId;
            data.restaurant = new ExpandoObject();
            if (restaurantCode != null)
            {
                data.restaurant.code = restaurantCode.Trim();
            }
            if (restaurantId != null)
            {
                data.restaurant.id = restaurantId;
            }

            Event(data);
        }

        private void Acknowledgement(long orderId, string restaurantCode, long? restaurantId)
        {
            dynamic data = new ExpandoObject();
            data.action = EventType.ACKNOWLEDGEMENT;
            data.order = orderId;
            data.restaurant = new ExpandoObject();
            if (restaurantCode != null)
            {
                data.restaurant.code = restaurantCode.Trim();
            }
            if (restaurantId != null)
            {
                data.restaurant.id = restaurantId;
            }

            Event(data);
        }

        private void StateChange(long orderId, string orderState, string restaurantCode, long? restaurantId)
        {
            dynamic data = new ExpandoObject();
            data.action = EventType.STATE_CHANGE;
            data.order = orderId;
            data.state = orderState;
            data.restaurant = new ExpandoObject();
            if (restaurantCode != null)
            {
                data.restaurant.code = restaurantCode.Trim();
            }
            if (restaurantId != null)
            {
                data.restaurant.id = restaurantId;
            }

            Event(data);
        }

        private void Warning(string eventCode, string eventDescription, string restaurantCode, long? restaurantId)
        {
            dynamic data = new ExpandoObject();
            data.action = EventType.WARNING;
            data.@event = eventCode;
            data.description = eventDescription;
            data.restaurant = new ExpandoObject();
            if (restaurantCode != null)
            {
                data.restaurant.code = restaurantCode.Trim();
            }
            if (restaurantId != null)
            {
                data.restaurant.id = restaurantId;
            }

            Event(data);
        }

        private void Error(string eventCode, string eventDescription, string restaurantCode, long? restaurantId)
        {
            dynamic data = new ExpandoObject();
            data.action = EventType.ERROR;
            data.@event = eventCode;
            data.description = eventDescription;
            data.restaurant = new ExpandoObject();
            if (restaurantCode != null)
            {
                data.restaurant.code = restaurantCode.Trim();
            }
            if (restaurantId != null)
            {
                data.restaurant.id = restaurantId;
            }

            Event(data);
        }

        private void Event(ExpandoObject data)
        {
            try
            {
                Request request = new Request();
                request.Endpoint = new Uri("events", UriKind.Relative);
                request.Body = data;
                Response response = Connection.Post<Dictionary<string, object>>(request);
                if (response.StatusCode != HttpStatusCode.OK)
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

        private class EventType
        {
            public const string INITIALIZATION = "INITIALIZATION";
            public const string HEART_BEAT = "HEART_BEAT";
            public const string RECEPTION = "RECEPTION";
            public const string ACKNOWLEDGEMENT = "ACKNOWLEDGEMENT";
            public const string STATE_CHANGE = "STATE_CHANGE";
            public const string WARNING = "WARNING";
            public const string ERROR = "ERROR";
        }
    }
}
