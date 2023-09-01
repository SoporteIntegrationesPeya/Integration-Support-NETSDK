///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using ReceptionSdk.Exceptions;
using ReceptionSdk.Helpers;
using ReceptionSdk.Http;
using ReceptionSdk.Models;
using ReceptionSdk.Utils;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;

namespace ReceptionSdk.Clients
{
    /// <summary>
    /// A client for Restaurants API.
    /// </summary>
    public class RestaurantsClient
    {

        private ApiConnection Connection { get; set; }

        /// <summary>
        /// Instantiate a new Resturants API client.
        /// </summary>
        /// <param name="connection">an API connection</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        public RestaurantsClient(ApiConnection connection)
        {
            Ensure.ArgumentNotNull(connection, "connection");

            Connection = connection;
        }

        /// <summary>
        /// Returns all the possible restaurants inside the pagination defined
        /// </summary>
        /// <param name="options">the pagination configuration</param>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>the list of restaurants</returns>
        /// <seealso cref="Restaurant"/>
        public IList<Restaurant> GetAll(PaginationOptions options)
        {
            try
            {
                Request request = new Request
                {
                    Endpoint = new Uri("restaurants", UriKind.Relative)
                };
                request.Parameters.offset = options.Offset;
                request.Parameters.limit = options.Limit;
                Response response = Connection.Get<GenericResponse<Restaurant>>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return response.Body.Data;
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
        /// Returns all the possible restaurants inside the pagination defined with the integrationCode defined
        /// </summary>
        /// <param name="options">the pagination configuration</param>
        /// <param name="integrationCode">the code of the restaurant</param>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>the list of restaurants</returns>
        /// <seealso cref="Restaurant"/>
        public IList<Restaurant> GetAll(PaginationOptions options, string integrationCode)
        {
            Ensure.ArgumentNotNullOrEmptyString(integrationCode, "integrationCode");
            try
            {
                Request request = new Request
                {
                    Endpoint = new Uri("restaurants", UriKind.Relative)
                };
                request.Parameters.offset = options.Offset;
                request.Parameters.limit = options.Limit;
                request.Parameters.code = integrationCode;
                Response response = Connection.Get<GenericResponse<Restaurant>>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return response.Body.Data;
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
        /// Opens the restaurant from the specified date
        /// </summary>
        /// <param name="restaurant">the restaurant to open</param>
        /// <param name="from">the date on wich it will be opened</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void Open(Restaurant restaurant, DateTime from)
        {
            Ensure.ArgumentNotNull(restaurant, "restaurant");
            Ensure.ArgumentNotNull(from, "from");

            Update(restaurant.Id, from, DateTime.MinValue, null);
        }

        /// <summary>
        /// Opens the restaurant from the specified date
        /// </summary>
        /// <param name="restaurant">the restaurant's id to open</param>
        /// <param name="from">the date on wich it will be opened</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void Open(long restaurantId, DateTime from)
        {
            Ensure.GreaterThanZero(restaurantId, "restaurantId");
            Ensure.ArgumentNotNull(from, "from");

            Update(restaurantId, from, DateTime.MinValue, null);
        }

        /// <summary>
        /// Closes the restaurant in the specified range
        /// </summary>
        /// <param name="restaurant">the restaurant to close</param>
        /// <param name="from">the init date on wich it will be closed</param>
        /// <param name="to">the end date on wich it will be closed</param>
        /// <param name="reason">the reason for the closure</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void Close(Restaurant restaurant, DateTime from, DateTime to, string reason)
        {
            Ensure.ArgumentNotNull(restaurant, "restaurant");
            Ensure.GreaterThanZero(restaurant.Id, "restaurant.Id");
            Ensure.ArgumentNotNull(from, "from");
            Ensure.ArgumentNotNull(to, "to");
            Ensure.ArgumentNotNullOrEmptyString(reason, "reason");
            Ensure.FirstDateBeforeSecond(from, to, "from", "to");

            Update(restaurant.Id, from, to, reason);
        }

        /// <summary>
        /// Closes the restaurant in the specified range
        /// </summary>
        /// <param name="restaurant">the restaurant's id to close</param>
        /// <param name="from">the init date on wich it will be closed</param>
        /// <param name="to">the end date on wich it will be closed</param>
        /// <param name="reason">the reason for the closure</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        public void Close(long restaurantId, DateTime from, DateTime to, string reason)
        {
            Ensure.GreaterThanZero(restaurantId, "restaurantId");
            Ensure.ArgumentNotNull(from, "from");
            Ensure.ArgumentNotNull(to, "to");
            Ensure.ArgumentNotNullOrEmptyString(reason, "reason");
            Ensure.FirstDateBeforeSecond(from, to, "from", "to");

            Update(restaurantId, from, to, reason);
        }

        private void Update(long restaurantId, DateTime from, DateTime to, string reason)
        {
            try
            {
                Request request = new Request
                {
                    Endpoint = new Uri($"restaurants/{restaurantId}", UriKind.Relative)
                };
                dynamic body = new ExpandoObject();
                if (to == DateTime.MinValue)
                {
                    body.state = RestaurantState.ONLINE;
                    body.from = DateUtil.FormatDate(from);
                }
                else
                {
                    body.state = RestaurantState.OFFLINE;
                    body.from = DateUtil.FormatDate(from);
                    body.to = DateUtil.FormatDate(to);
                    body.reason = reason;
                }
                request.Body = body;
                Response response = Connection.Put<Dictionary<string, object>>(request);
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

    }
}
