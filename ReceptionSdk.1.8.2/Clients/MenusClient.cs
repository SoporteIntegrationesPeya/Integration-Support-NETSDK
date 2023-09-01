///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using ReceptionSdk.Http;
using ReceptionSdk.Helpers;
using System;
using ReceptionSdk.Utils;
using ReceptionSdk.Exceptions;
using System.Collections.Generic;
using ReceptionSdk.Models;
using ReceptionSdk.Enums;
using System.Dynamic;

namespace ReceptionSdk.Clients
{
    /// <summary>
    /// A client for Menus API.
    /// </summary>
    public abstract class MenusClient
    {
        private const string HEADER_PARTNER = "Peya-Partner-Id";

        private const string HEADER_VERSION = "Peya-Sdk-Version";

        private const string HEADER_SYSTEM = "Peya-Reception-System-Code";

        private ApiConnection Connection { get; set; }

        public abstract ExpandoObject GetItemPayload(object menuItem, long restaurantId);

        public abstract ExpandoObject GetItemSchedulePayload(Schedule scheduleItem, long restaurantId);

        public abstract String GetItemRoute();

        public abstract String GetItemsRoute(object menuItem);

        public abstract String GetItemByNameRoute(object menuItem);

        public abstract String GetItemByIntegrationCodeRoute(object menuItem);

        public abstract String GetItemSchedulesRoute(Schedule scheduleItem);

        /// <summary>
        /// Instantiate a new Menus API client.
        /// </summary>
        /// <param name="connection">an API connection</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        public MenusClient(ApiConnection connection)
        {
            Ensure.ArgumentNotNull(connection, "connection");

            Connection = connection;
        }

        /// <summary>
        /// Create the menuItem.
        /// </summary>
        /// <param name="menuItem">new menuItem to be created</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{bool} if the menuItem was created</returns>
        public bool Create(object menuItem)
        {
            Ensure.ArgumentNotNull(menuItem, "menuItem");

            Connection.DoAuthenticate();
            if (Connection.Credentials.CentralizedKeys())
            {
                throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
            }
            return CreateItem(menuItem, Connection.RestaurantId);
        }

        /// <summary>
        /// Create the menuItem with the specified restaurant code.
        /// </summary>
        /// <param name="menuItem">new menuItem to be created</param>
        /// <param name="restaurantCode">restaurant code when using centralized keys</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{bool} if the menuItem was created</returns>
        public bool Create(object menuItem, string restaurantCode)
        {
            Ensure.ArgumentNotNull(menuItem, "menuItem");
            Ensure.ArgumentNotNullOrEmptyString(restaurantCode, "restaurantCode");

            Connection.DoAuthenticate();
            long restaurantId = PartnerUtils.matchRestaurantByIntegrationCode(Connection, restaurantCode);
            try
            {
                return RunOperationByIntegrationCode(menuItem, restaurantCode, OperationType.CREATE);
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
        /// Create the menuItem with the specified restaurant id.
        /// </summary>
        /// <param name="menuItem">new menuItem to be created</param>
        /// <param name="restaurantId">restaurant id when using centralized keys</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{bool} if the menuItem was created</returns>
        public bool Create(object menuItem, long restaurantId)
        {
            Ensure.ArgumentNotNull(menuItem, "menuItem");
            Ensure.GreaterThanZero(restaurantId, "restaurantId");

            Connection.DoAuthenticate();
            return CreateItem(menuItem, restaurantId);
        }
        private bool CreateItem(object menuItem, long restaurantId)
        {
            try
            {
                Request request = BuildRequestItem(menuItem, restaurantId);
                dynamic headers = AddGeneralHeaders(restaurantId);
                request.Headers = headers;

                Response response = Connection.Post<Dictionary<string, object>>(request);
                return HttpUtils.ResponseIsSuccessful(response);
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

        private dynamic AddGeneralHeaders(long restaurantId)
        {
            dynamic headers = new ExpandoObject();
            ((IDictionary<String, Object>)headers).Add(HEADER_PARTNER, restaurantId);
            ((IDictionary<String, Object>)headers).Add(HEADER_VERSION, EventsClient.Version);
            return headers;
        }

        private Request BuildRequestItem(object menuItem, long restaurantId)
        {
            Request request = new Request
            {
                Endpoint = new Uri($"{Connection.ISMSUrl()}{GetItemRoute()}", UriKind.Absolute),
                Timeout = TimeSpan.FromMinutes(1),
                Body = GetItemPayload(menuItem, restaurantId)
            };
            return request;
        }

        /// <summary>
        /// Update the menuItem.
        /// </summary>
        /// <param name="menuItem">new menuItem to be updated</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{bool} if the menuItem was updated</returns>
        public bool Update(object menuItem)
        {
            Ensure.ArgumentNotNull(menuItem, "menuItem");

            Connection.DoAuthenticate();
            if (Connection.Credentials.CentralizedKeys())
            {
                throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
            }
            return UpdateItem(menuItem, Connection.RestaurantId);
        }

        /// <summary>
        /// Update the menuItem with the specified restaurant code.
        /// </summary>
        /// <param name="menuItem">new menuItem to be updated</param>
        /// <param name="restaurantCode">restaurant code when using centralized keys</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{bool} if the menuItem was updated</returns>
        public bool Update(object menuItem, string restaurantCode)
        {
            Ensure.ArgumentNotNull(menuItem, "menuItem");
            Ensure.ArgumentNotNullOrEmptyString(restaurantCode, "restaurantCode");

            Connection.DoAuthenticate();
            long restaurantId = PartnerUtils.matchRestaurantByIntegrationCode(Connection, restaurantCode);
            try
            {
                return RunOperationByIntegrationCode(menuItem, restaurantCode, OperationType.UPDATE);
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
        /// Update the menuItem with the specified restaurant id.
        /// </summary>
        /// <param name="menuItem">new menuItem to be updated</param>
        /// <param name="restaurantId">restaurant id when using centralized keys</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{bool} if the menuItem was updated</returns>
        public bool Update(object menuItem, long restaurantId)
        {
            Ensure.GreaterThanZero(restaurantId, "restaurantId");

            Connection.DoAuthenticate();
            return UpdateItem(menuItem, restaurantId);
        }

        private bool UpdateItem(object menuItem, long restaurantId)
        {
            try
            {
                Request request = BuildRequestItem(menuItem, restaurantId);
                dynamic headers = AddGeneralHeaders(restaurantId);
                request.Headers = headers;

                Response response = Connection.Put<Dictionary<string, object>>(request);
                return HttpUtils.ResponseIsSuccessful(response);
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
        /// Delete the menuItem.
        /// </summary>
        /// <param name="menuItem">new menuItem to be deleted</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{bool} if the menuItem was deleted</returns>
        public bool Delete(object menuItem)
        {
            Connection.DoAuthenticate();
            if (Connection.Credentials.CentralizedKeys())
            {
                throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
            }
            return DeleteItem(menuItem, Connection.RestaurantId);
        }

        /// <summary>
        /// Delete the menuItem with the specified restaurant code.
        /// </summary>
        /// <param name="menuItem">new menuItem to be deleted</param>
        /// <param name="restaurantCode">restaurant code when using centralized keys</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{bool} if the menuItem was deleted</returns>
        public bool Delete(object menuItem, string restaurantCode)
        {
            Ensure.ArgumentNotNull(menuItem, "menuItem");
            Ensure.ArgumentNotNullOrEmptyString(restaurantCode, "restaurantCode");

            Connection.DoAuthenticate();
            long restaurantId = PartnerUtils.matchRestaurantByIntegrationCode(Connection, restaurantCode);
            try
            {
                return RunOperationByIntegrationCode(menuItem, restaurantCode, OperationType.DELETE);
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
        /// Delete the menuItem with the specified restaurant id.
        /// </summary>
        /// <param name="menuItem">new menuItem to be deleted</param>
        /// <param name="restaurantId">restaurant id when using centralized keys</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{bool} if the menuItem was deleted</returns>
        public bool Delete(object menuItem, long restaurantId)
        {
            Ensure.GreaterThanZero(restaurantId, "restaurantId");

            Connection.DoAuthenticate();
            return DeleteItem(menuItem, restaurantId);
        }

        private bool DeleteItem(object menuItem, long restaurantId)
        {
            try
            {
                Request request = BuildRequestItem(menuItem, restaurantId);
                dynamic headers = AddGeneralHeaders(restaurantId);
                request.Headers = headers;

                Response response = Connection.Delete<Dictionary<string, object>>(request);
                return HttpUtils.ResponseIsSuccessful(response);
            }
            catch (ApiException aex)
            {
                throw aex;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        private bool RunOperationByIntegrationCode(object menuItem, String restaurantCode, OperationType operationType)
        {

            List<Restaurant> restaurants = PartnerUtils.GetRestaurants(Connection);
            foreach (Restaurant restaurant in restaurants)
            {
                if (restaurant.IntegrationCode.Equals(restaurantCode))
                {
                    switch (operationType)
                    {
                        case OperationType.CREATE:
                            return CreateItem(menuItem, restaurant.Id);
                        case OperationType.UPDATE:
                            return UpdateItem(menuItem, restaurant.Id);
                        case OperationType.DELETE:
                            return DeleteItem(menuItem, restaurant.Id);
                    }
                }
            }
            throw new ApiException(ErrorCode.NOT_EXISTS);
        }

        /// <summary>
        /// Get all the menuItem
        /// </summary>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{menuItems} if the menuItems exists</returns>
        public IList<object> GetAll(object menuItem)
        {
            Connection.DoAuthenticate();
            if (Connection.Credentials.CentralizedKeys())
            {
                throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
            }

            return GetAllItem(menuItem, Connection.RestaurantId);
        }

        /// <summary>
        /// Get the menuItem with the specified restaurant code.
        /// </summary>
        /// <param name="restaurantCode">restaurant code when using centralized keys</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{menuItems} if the menuItems exists</returns>
        public IList<object> GetAll(object menuItem, string restaurantCode)
        {
            Ensure.ArgumentNotNullOrEmptyString(restaurantCode, "restaurantCode");

            Connection.DoAuthenticate();
            long restaurantId = PartnerUtils.matchRestaurantByIntegrationCode(Connection, restaurantCode);
            try
            {
                return RunGetAllOperationByIntegrationCode(menuItem, restaurantCode);
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
        /// Create the menuItem with the specified restaurant id.
        /// </summary>
        /// <param name="restaurantId">restaurant id when using centralized keys</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{menuItems} if the menuItems exists</returns>
        public IList<object> GetAll(object menuItem, long restaurantId)
        {
            Ensure.GreaterThanZero(restaurantId, "restaurantId");

            Connection.DoAuthenticate();
            return GetAllItem(menuItem, restaurantId);
        }
        private IList<object> GetAllItem(object menuItem, long restaurantId)
        {
            try
            {
                Request request = BuildRequestItems(menuItem, restaurantId);
                dynamic headers = AddGeneralHeaders(restaurantId);
                request.Headers = headers;

                Response response = Connection.Get<GenericResponse<object>>(request);
                return HttpUtils.ResponseData(response);
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

        private Request BuildRequestItems(object menuItem, long restaurantId)
        {
            Request request = new Request
            {
                Endpoint = new Uri($"{Connection.ISMSUrl()}{GetItemsRoute(menuItem)}", UriKind.Absolute),
                Timeout = TimeSpan.FromMinutes(1)
            };
            return request;
        }

        /// <summary>
        /// Get the menuItem by name
        /// </summary>
        /// <param name="menuItem">menuItem to get</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{menuItem} if the menuItem exists</returns>
        public object GetByName(object menuItem)
        {
            Ensure.ArgumentNotNull(menuItem, "menuItem");

            Connection.DoAuthenticate();
            if (Connection.Credentials.CentralizedKeys())
            {
                throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
            }
            return GetByNameItem(menuItem, Connection.RestaurantId);
        }

        /// <summary>
        /// Get the menuItem by name with the specified restaurant code.
        /// </summary>
        /// <param name="menuItem">menuItem to get</param>
        /// <param name="restaurantCode">restaurant code when using centralized keys</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{menuItem} if the menuItem exists</returns>
        public object GetByName(object menuItem, string restaurantCode)
        {
            Ensure.ArgumentNotNull(menuItem, "menuItem");
            Ensure.ArgumentNotNullOrEmptyString(restaurantCode, "restaurantCode");

            Connection.DoAuthenticate();
            long restaurantId = PartnerUtils.matchRestaurantByIntegrationCode(Connection, restaurantCode);
            try
            {
                return RunGetOperationByIntegrationCode(menuItem, restaurantCode, OperationType.GET_BY_NAME);
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
        ///Get the menuItem by name with the specified restaurant id.
        /// </summary>
        /// <param name="menuItem">menuItem to get</param>
        /// <param name="restaurantId">restaurant id when using centralized keys</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{menuItem} if the menuItem exists</returns>
        public object GetByName(object menuItem, long restaurantId)
        {
            Ensure.ArgumentNotNull(menuItem, "menuItem");
            Ensure.GreaterThanZero(restaurantId, "restaurantId");

            Connection.DoAuthenticate();
            return GetByNameItem(menuItem, restaurantId);
        }
        private object GetByNameItem(object menuItem, long restaurantId)
        {
            try
            {
                Request request = BuildRequestGetByNameItem(menuItem, restaurantId);
                dynamic headers = AddGeneralHeaders(restaurantId);
                request.Headers = headers;

                Response response = Connection.Get<object>(request);
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

        private Request BuildRequestGetByNameItem(object menuItem, long restaurantId)
        {
            Request request = new Request
            {
                Endpoint = new Uri($"{Connection.ISMSUrl()}{GetItemByNameRoute(menuItem)}", UriKind.Absolute),
                Timeout = TimeSpan.FromMinutes(1)
            };
            return request;
        }

        /// <summary>
        /// Get the menuItem by integrationCode
        /// </summary>
        /// <param name="menuItem">menuItem to get</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{menuItem} if the menuItem exists</returns>
        public object GetByIntegrationCode(object menuItem)
        {
            Ensure.ArgumentNotNull(menuItem, "menuItem");

            Connection.DoAuthenticate();
            if (Connection.Credentials.CentralizedKeys())
            {
                throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
            }
            return GetByIntegrationCodeItem(menuItem, Connection.RestaurantId);
        }

        /// <summary>
        /// Get the menuItem by integrationCode with the specified restaurant code.
        /// </summary>
        /// <param name="menuItem">menuItem to get</param>
        /// <param name="restaurantCode">restaurant code when using centralized keys</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{menuItem} if the menuItem exists</returns>
        public object GetByIntegrationCode(object menuItem, string restaurantCode)
        {
            Ensure.ArgumentNotNull(menuItem, "menuItem");
            Ensure.ArgumentNotNullOrEmptyString(restaurantCode, "restaurantCode");

            Connection.DoAuthenticate();
            long restaurantId = PartnerUtils.matchRestaurantByIntegrationCode(Connection, restaurantCode);
            try
            {
                return RunGetOperationByIntegrationCode(menuItem, restaurantCode, OperationType.GET_BY_INTEGRATION_CODE);
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
        ///Get the menuItem by integrationCode with the specified restaurant id.
        /// </summary>
        /// <param name="menuItem">menuItem to get</param>
        /// <param name="restaurantId">restaurant id when using centralized keys</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{menuItem} if the menuItem exists</returns>
        public object GetByIntegrationCode(object menuItem, long restaurantId)
        {
            Ensure.ArgumentNotNull(menuItem, "menuItem");
            Ensure.GreaterThanZero(restaurantId, "restaurantId");

            Connection.DoAuthenticate();
            return GetByIntegrationCodeItem(menuItem, restaurantId);
        }
        private object GetByIntegrationCodeItem(object menuItem, long restaurantId)
        {
            try
            {
                Request request = BuildRequestGetByIntegrationCodeItem(menuItem, restaurantId);
                dynamic headers = AddGeneralHeaders(restaurantId);
                request.Headers = headers;

                Response response = Connection.Get<object>(request);
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

        private Request BuildRequestGetByIntegrationCodeItem(object menuItem, long restaurantId)
        {
            Request request = new Request
            {
                Endpoint = new Uri($"{Connection.ISMSUrl()}{GetItemByIntegrationCodeRoute(menuItem)}", UriKind.Absolute),
                Timeout = TimeSpan.FromMinutes(1)
            };
            return request;
        }
        private object RunGetOperationByIntegrationCode(object menuItem, String restaurantCode, OperationType operationType)
        {
            List<Restaurant> restaurants = PartnerUtils.GetRestaurants(Connection);
            foreach (Restaurant restaurant in restaurants)
            {
                if (restaurant.IntegrationCode.Equals(restaurantCode))
                {
                    switch (operationType)
                    {
                        case OperationType.GET_ALL:
                            return GetAllItem(menuItem, restaurant.Id);
                        case OperationType.GET_BY_NAME:
                            return GetByNameItem(menuItem, restaurant.Id);
                        case OperationType.GET_BY_INTEGRATION_CODE:
                            return GetByIntegrationCodeItem(menuItem, restaurant.Id);
                    }
                }
            }
            throw new ApiException(ErrorCode.INTERNAL_SERVER_ERROR);
        }

        private IList<object> RunGetAllOperationByIntegrationCode(object menuItem, String restaurantCode)
        {

            List<Restaurant> restaurants = PartnerUtils.GetRestaurants(Connection);
            foreach (Restaurant restaurant in restaurants)
            {
                if (restaurant.IntegrationCode.Equals(restaurantCode))
                {
                    return GetAllItem(menuItem, restaurant.Id);
                        
                }
            }
            throw new ApiException(ErrorCode.INTERNAL_SERVER_ERROR);
        }

        /// <summary>
        /// Create the schedule.
        /// </summary>
        /// <param name="scheduleItem">new menuItem to be created</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{bool} if the scheduleItem was created</returns>
        public bool CreateSchedule(Schedule scheduleItem)
        {
            Ensure.ArgumentNotNull(scheduleItem, "scheduleItem");

            Connection.DoAuthenticate();
            if (Connection.Credentials.CentralizedKeys())
            {
                throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
            }
            return CreateItemSchedule(scheduleItem, Connection.RestaurantId);
        }

        /// <summary>
        /// Create the schedule with the specified restaurant code.
        /// </summary>
        /// <param name="scheduleItem">new scheduleItem to be created</param>
        /// <param name="restaurantCode">restaurant code when using centralized keys</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{bool} if the scheduleItem was created</returns>
        public bool CreateSchedule(Schedule scheduleItem, string restaurantCode)
        {
            Ensure.ArgumentNotNull(scheduleItem, "scheduleItem");
            Ensure.ArgumentNotNullOrEmptyString(restaurantCode, "restaurantCode");

            Connection.DoAuthenticate();
            long restaurantId = PartnerUtils.matchRestaurantByIntegrationCode(Connection, restaurantCode);
            try
            {
                return RunOperationScheduleByIntegrationCode(scheduleItem, restaurantCode, OperationType.CREATE);
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
        /// Create the schedule with the specified restaurant id.
        /// </summary>
        /// <param name="scheduleItem">new scheduleItem to be created</param>
        /// <param name="restaurantId">restaurant id when using centralized keys</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{bool} if the scheduleItem was created</returns>
        public bool CreateSchedule(Schedule scheduleItem, long restaurantId)
        {
            Ensure.ArgumentNotNull(scheduleItem, "scheduleItem");
            Ensure.GreaterThanZero(restaurantId, "restaurantId");

            Connection.DoAuthenticate();
            return CreateItemSchedule(scheduleItem, restaurantId);
        }
        private bool CreateItemSchedule(Schedule scheduleItem, long restaurantId)
        {
            try
            {
                Request request = BuildRequestItemSchedule(scheduleItem, restaurantId);
                dynamic headers = AddGeneralHeaders(restaurantId);
                request.Headers = headers;

                Response response = Connection.Post<Dictionary<string, object>>(request);
                return HttpUtils.ResponseIsSuccessful(response);
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

        private Request BuildRequestItemSchedule(Schedule scheduleItem, long restaurantId)
        {
            Request request = new Request
            {
                Endpoint = new Uri($"{Connection.ISMSUrl()}{GetItemRoute()}/schedule", UriKind.Absolute),
                Timeout = TimeSpan.FromMinutes(1),
                Body = GetItemSchedulePayload(scheduleItem, restaurantId)
            };
            return request;
        }

        private bool RunOperationScheduleByIntegrationCode(Schedule scheduleItem, String restaurantCode, OperationType operationType)
        {

            List<Restaurant> restaurants = PartnerUtils.GetRestaurants(Connection);
            foreach (Restaurant restaurant in restaurants)
            {
                if (restaurant.IntegrationCode.Equals(restaurantCode))
                {
                    switch (operationType)
                    {
                        case OperationType.CREATE:
                            return CreateItemSchedule(scheduleItem, restaurant.Id);
                        case OperationType.DELETE:
                            return CreateItemSchedule(scheduleItem, restaurant.Id);
                    }
                }
            }
            throw new ApiException(ErrorCode.INTERNAL_SERVER_ERROR);
        }

        /// <summary>
        /// Delete the schedule.
        /// </summary>
        /// <param name="scheduleItem">scheduleItem to be deleted</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{bool} if the scheduleItem was deleted</returns>
        public bool DeleteSchedule(Schedule scheduleItem)
        {
            Ensure.ArgumentNotNull(scheduleItem, "scheduleItem");

            Connection.DoAuthenticate();
            if (Connection.Credentials.CentralizedKeys())
            {
                throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
            }
            return DeleteItemSchedule(scheduleItem, Connection.RestaurantId);
        }

        /// <summary>
        /// Delete the schedule with the specified restaurant code.
        /// </summary>
        /// <param name="scheduleItem">scheduleItem to be deleted</param>
        /// <param name="restaurantCode">restaurant code when using centralized keys</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{bool} if the scheduleItem was deleted</returns>
        public bool DeleteSchedule(Schedule scheduleItem, string restaurantCode)
        {
            Ensure.ArgumentNotNull(scheduleItem, "scheduleItem");
            Ensure.ArgumentNotNullOrEmptyString(restaurantCode, "restaurantCode");

            Connection.DoAuthenticate();
            long restaurantId = PartnerUtils.matchRestaurantByIntegrationCode(Connection, restaurantCode);
            try
            {
                return RunOperationScheduleByIntegrationCode(scheduleItem, restaurantCode, OperationType.DELETE);
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
        /// Delete the schedule with the specified restaurant id.
        /// </summary>
        /// <param name="scheduleItem">scheduleItem to be deleted</param>
        /// <param name="restaurantId">restaurant id when using centralized keys</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{bool} if the scheduleItem was deleted</returns>
        public bool DeleteSchedule(Schedule scheduleItem, long restaurantId)
        {
            Ensure.ArgumentNotNull(scheduleItem, "scheduleItem");
            Ensure.GreaterThanZero(restaurantId, "restaurantId");

            Connection.DoAuthenticate();
            return DeleteItemSchedule(scheduleItem, restaurantId);
        }

        private bool DeleteItemSchedule(Schedule scheduleItem, long restaurantId)
        {
            try
            {
                Request request = BuildRequestItemSchedule(scheduleItem, restaurantId);
                dynamic headers = AddGeneralHeaders(restaurantId);
                request.Headers = headers;

                Response response = Connection.Delete<Dictionary<string, object>>(request);
                return HttpUtils.ResponseIsSuccessful(response);
            }
            catch (ApiException aex)
            {
                throw aex;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        /// <summary>
        /// Get all the schedules
        /// </summary>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{schedules} if the scheduleItem exists</returns>
        public IList<object> GetAllSchedule(Schedule scheduleItem)
        {
            Ensure.ArgumentNotNull(scheduleItem, "scheduleItem");

            Connection.DoAuthenticate();
            if (Connection.Credentials.CentralizedKeys())
            {
                throw new ArgumentNullException("You must specify a restaurantCode or restaurantId");
            }
            return GetAllItemSchedule(scheduleItem, Connection.RestaurantId);
        }

        /// <summary>
        /// Get the schedules with the specified restaurant code.
        /// </summary>
        /// <param name="restaurantCode">restaurant code when using centralized keys</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{schedules} if the scheduleItem exists</returns>
        public IList<object> GetAllSchedule(Schedule scheduleItem, string restaurantCode)
        {
            Ensure.ArgumentNotNull(scheduleItem, "scheduleItem");
            Ensure.ArgumentNotNullOrEmptyString(restaurantCode, "restaurantCode");

            Connection.DoAuthenticate();
            long restaurantId = PartnerUtils.matchRestaurantByIntegrationCode(Connection, restaurantCode);
            try
            {
                return RunGetAllOperationScheduleByIntegrationCode(scheduleItem, restaurantCode);
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
        /// Get the schedules with the specified restaurant id.
        /// </summary>
        /// <param name="restaurantId">restaurant id when using centralized keys</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>{schedules} if the scheduleItem exists</returns>
        public IList<object> GetAllSchedule(Schedule scheduleItem, long restaurantId)
        {
            Ensure.ArgumentNotNull(scheduleItem, "scheduleItem");
            Ensure.GreaterThanZero(restaurantId, "restaurantId");

            Connection.DoAuthenticate();
            return GetAllItemSchedule(scheduleItem, restaurantId);
        }
        private IList<object> GetAllItemSchedule(Schedule scheduleItem, long restaurantId)
        {
            try
            {
                Request request = BuildRequestItemsSchedule(scheduleItem, restaurantId);
                dynamic headers = AddGeneralHeaders(restaurantId);
                request.Headers = headers;

                Response response = Connection.Get<GenericResponse<object>>(request);
                return HttpUtils.ResponseData(response);
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

        private Request BuildRequestItemsSchedule(Schedule scheduleItem, long restaurantId)
        {
            Request request = new Request
            {
                Endpoint = new Uri($"{Connection.ISMSUrl()}{GetItemSchedulesRoute(scheduleItem)}", UriKind.Absolute),
                Timeout = TimeSpan.FromMinutes(1)
            };
            return request;
        }

        private IList<object> RunGetAllOperationScheduleByIntegrationCode(Schedule scheduleItem, String restaurantCode)
        {

            List<Restaurant> restaurants = PartnerUtils.GetRestaurants(Connection);
            foreach (Restaurant restaurant in restaurants)
            {
                if (restaurant.IntegrationCode.Equals(restaurantCode))
                {
                    return GetAllItemSchedule(scheduleItem, restaurant.Id);

                }
            }
            throw new ApiException(ErrorCode.INTERNAL_SERVER_ERROR);
        }
        
        /// <summary>
        /// Get all GTINS.
        /// </summary>
        /// <param name="pagination">Pagination option </param>
        /// <param name="vendorId">vendor id when using centralized keys</param>
        /// <returns>{GetAllGtinsList} for an specific vendor</returns>
        public IList<object> GetAllGtins(PaginationOptions pagination, long vendorId)
        {
            Ensure.ArgumentNotNull(pagination, "pagination");
            Ensure.GreaterThanZero(vendorId, "vendorId");
            Connection.DoAuthenticate();
            return GetAllGtinsList(pagination, vendorId);
        }

        private IList<object> GetAllGtinsList(PaginationOptions pagination, long vendorId)
        {
            try
            {
                Request request = BuildRequestGetGtins(pagination);
                dynamic headers = AddGtinHeaders(vendorId);
                request.Headers = headers;
                Response response = Connection.Get<GenericResponse<object>>(request);
                return HttpUtils.ResponseData(response);
            }
            catch (ApiException ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }
        
        /// <summary>
        /// Get all GTINS with the specified name.
        /// </summary>
        /// <param name="name">product name</param>
        /// <param name="pagination">Pagination option </param>
        /// <param name="vendorId">vendor id when using centralized keys</param>
        /// <returns>{GetGtinsByNameList} if the name exists</returns>
        public IList<object> GetGtinsByName(String name, PaginationOptions pagination, long vendorId)
        {
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(pagination, "pagination");
            Ensure.GreaterThanZero(vendorId, "vendorId");
            Connection.DoAuthenticate();
            return GetGtinsByNameList(name, pagination, vendorId);
        }
        
        private IList<object> GetGtinsByNameList(String name, PaginationOptions pagination, long vendorId)
        {
            try
            {
                Request request = BuildRequestGetGtinsByName(name, pagination);
                dynamic headers = AddGtinHeaders(vendorId);
                request.Headers = headers;
                Response response = Connection.Get<GenericResponse<object>>(request);
                return HttpUtils.ResponseData(response);
                
            }catch (ApiException ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }


        private Request BuildRequestGetGtins(PaginationOptions pagination)
        {
            int size = pagination.Limit;
            int page = pagination.Offset;
            Request request = new Request
            {
                Endpoint = new Uri($"{Connection.ISMSUrl()}product/allGtinDictionary?size=${size}&page=${page}", UriKind.Absolute),
                Timeout = TimeSpan.FromMinutes(1),
            };
            return request;
        }
        
        private Request BuildRequestGetGtinsByName(String name, PaginationOptions pagination)
        {
            int size = pagination.Limit;
            int page = pagination.Offset;
            Request request = new Request
            {
                Endpoint = new Uri($"{Connection.ISMSUrl()}product/name/{name}/allGtinDictionary?size=${size}&page=${page}", UriKind.Absolute),
                Timeout = TimeSpan.FromMinutes(1),
            };
            return request;
        }
        
        private dynamic AddGtinHeaders(long vendorId)
        {
            dynamic headers = new ExpandoObject();
            ((IDictionary<String, Object>)headers).Add(HEADER_PARTNER, vendorId);
            ((IDictionary<String, Object>)headers).Add(HEADER_VERSION, EventsClient.Version);
            ((IDictionary<String, Object>)headers).Add(HEADER_SYSTEM, Connection.Credentials.ClientId);
            
            return headers;
        }
        
        
        
        


    }
}
