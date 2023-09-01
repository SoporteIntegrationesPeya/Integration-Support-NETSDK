using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using ReceptionSdk.Exceptions;
using ReceptionSdk.Helpers;
using ReceptionSdk.Http;
using ReceptionSdk.Models;
using ReceptionSdk.Utils;

namespace ReceptionSdk.Clients
{
    public class PromotionClient
    {
        private ApiConnection Connection { get; set; }
        private const string HEADER_PARTNER = "Peya-Partner-Id";
        private const string HEADER_RECEPTION_SYSTEM = "Peya-Reception-System-Code";
        private const string HEADER_VERSION = "Peya-Sdk-Version";

        public PromotionClient(ApiConnection connection)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Connection = connection;
        }
        
        public object GetPromotion(string kondoId, long partnerId)
        {
            Ensure.ArgumentNotNull(kondoId, "KondoId");
            Ensure.GreaterThanZero(partnerId, "partnerId");
            Connection.DoAuthenticate();
            return GetPromotionById(kondoId, partnerId);
        }

        private object GetPromotionById(string kondoId, long partnerId)
        {
            try
            {
                Request request = BuildRequestGetById(kondoId, partnerId);
                dynamic headers = AddGeneralHeaders(partnerId);
                request.Headers = headers;
                Response response = Connection.Get<object>(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApiException(response);

                }
                return response.Content;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public object Create(Promotion promotion, long partnerId)
        {
            Ensure.ArgumentNotNull(promotion, "promotion");
            Ensure.GreaterThanZero(partnerId, "partnerId");
            Connection.DoAuthenticate();
            return CreatePromotion(promotion, partnerId);
        }

        private object CreatePromotion(Promotion promotion, long partnerId)
        {
            try
            {
                Request request = BuildRequestPromotion(promotion, partnerId);
                dynamic headers = AddGeneralHeaders(partnerId);
                request.Headers = headers;
                Response response = Connection.Post<Dictionary<string, object>>(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApiException(response);

                }
                return HttpUtils.ResponseBody(response);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public object Update(Promotion promotion, string kondoId, long partnerId)
        {
            Ensure.ArgumentNotNull(promotion, "promotion");
            Ensure.ArgumentNotNull(kondoId, "kondoId");
            Ensure.GreaterThanZero(partnerId, "partnerId");
            Connection.DoAuthenticate();
            return UpdatePromotion(promotion, kondoId, partnerId);
        }

        private object UpdatePromotion(Promotion promotion, string kondoId, long partnerId)
        {
            try
            {
                Request request = BuildRequestUpdatePromotion(promotion, kondoId, partnerId);
                dynamic headers = AddGeneralHeaders(partnerId);
                request.Headers = headers;
                Response response = Connection.Put<Dictionary<string, object>>(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApiException(response);
                }
                return HttpUtils.ResponseBody(response);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public Boolean Delete(string kondoId, long partnerId)
        {
            Ensure.GreaterThanZero(partnerId, "partnerId");
            Connection.DoAuthenticate();
            return DeletePromotion(kondoId,partnerId);
        }

        private Boolean DeletePromotion(string kondoId, long partnerId)
        {
            try
            {
                Request request = BuildRequestGetById(kondoId, partnerId);
                dynamic headers = AddGeneralHeaders(partnerId);
                request.Headers = headers;
                Response response = Connection.Delete<object>(request);
                if (response.StatusCode != HttpStatusCode.OK || response.StatusCode != HttpStatusCode.NoContent)
                {
                    throw new ApiException(response);
                }
                return HttpUtils.ResponseIsSuccessful(response);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        private Request BuildRequestGetById(string kondoId, long partnerId)
        {
            Request request = new Request
            {
                Endpoint = new Uri($"{Connection.ISMSUrl()}promotion/{kondoId}", UriKind.Absolute),
                Timeout = TimeSpan.FromMinutes(1),
            };
            return request;
        }
        
        private Request BuildRequestPromotion(Promotion promotion, long partnerId)
        {
            Request request = new Request
            {
                Endpoint = new Uri($"{Connection.ISMSUrl()}promotion", UriKind.Absolute),
                Timeout = TimeSpan.FromMinutes(1),
                Body = promotion,
            };
            return request;
        }

        private Request BuildRequestUpdatePromotion(Promotion promotion, string kondoId, long partnerId)
        {
            Request request = new Request
            {
                Endpoint = new Uri($"{Connection.ISMSUrl()}promotion/{kondoId}", UriKind.Absolute),
                Timeout = TimeSpan.FromMinutes(1),
                Body = promotion,
            };
            return request;
        }


        private dynamic AddGeneralHeaders(long partnerId)
        {
            dynamic headers = new ExpandoObject();
            ((IDictionary<String, Object>) headers).Add(HEADER_PARTNER, partnerId);
            ((IDictionary<String, Object>) headers).Add(HEADER_VERSION, EventsClient.Version);
            ((IDictionary<String, Object>) headers).Add(HEADER_RECEPTION_SYSTEM, Connection.Credentials.ClientId);

            return headers;
        }
    }
}