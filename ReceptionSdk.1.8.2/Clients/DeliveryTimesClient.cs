///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using ReceptionSdk.Exceptions;
using ReceptionSdk.Http;
using ReceptionSdk.Models;
using ReceptionSdk.Helpers;
using System.Collections.Generic;
using System;
using System.Net;

namespace ReceptionSdk.Clients
{
    /// <summary>
    /// A client for Delivery Times API.
    /// </summary>
    public class DeliveryTimesClient
    {
        private ApiConnection Connection { get; set; }

        /// <summary>
        /// Instantiate a new Delivery Times API client.
        /// </summary>
        /// <param name="connection">an API connection</param>
        /// <exception cref="ArgumentException">if some error has occurred</exception>
        public DeliveryTimesClient(ApiConnection connection)
        {
            Ensure.ArgumentNotNull(connection, "connection");

            Connection = connection;
        }

        /// <summary>
        /// Returns all the possible delivery times
        /// </summary>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>the list of delivery times</returns>
        /// <seealso cref="DeliveryTime"/>
        public IList<DeliveryTime> GetAll()
        {
            try
            {
                Request request = new Request();
                request.Endpoint = new Uri("deliveryTimes", UriKind.Relative);
                List<DeliveryTime> deliveryTimes = new List<DeliveryTime>();
                Response response = Connection.Get<DeliveryTimesResponse>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (response.Body.DeliveryTimes.Count == response.Body.Total)
                    {
                        return response.Body.DeliveryTimes;
                    }
                    else
                    {
                        deliveryTimes.AddRange(response.Body.DeliveryTimes);
                        while (response.Body.DeliveryTimes != null && response.Body.DeliveryTimes.Count != 0)
                        {
                            request.Endpoint = new Uri("deliveryTimes", UriKind.Relative);
                            request.Parameters.offset = deliveryTimes.Count;
                            response = Connection.Get<DeliveryTimesResponse>(request);
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                deliveryTimes.AddRange(response.Body.DeliveryTimes);
                            }
                            else
                            {
                                throw new ApiException(response);
                            }
                        }
                        return deliveryTimes;
                    }
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
    }
}
