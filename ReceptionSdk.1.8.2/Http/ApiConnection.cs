///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using ReceptionSdk.Helpers;
using ReceptionSdk.Exceptions;
using System.Collections.Generic;
using System;
using System.Dynamic;
using System.Net;
using Newtonsoft.Json.Linq;

namespace ReceptionSdk.Http
{
    /// <summary>
    /// API REST client
    /// </summary>
    public class ApiConnection
    {
        /// <summary>
        /// Service access credentials
        /// </summary>
        public Credentials Credentials { get; set; }
        private Connection Connection { get; set; }
        private Connection ConnectionISMS { get; set; }
        private string Token { get; set; }
        private DateTime? TokenAge { get; set; }
        private int tokenTimeout = 60;
        public long RestaurantId { get; set; }


        /// <summary>
        /// The service token timeout.
        /// Actual default version is 60 minutes.
        /// Probably you don't wanna change this value unless you know what are you doing
        /// </summary>
        public int TokenTimeout
        {
            get
            {
                return tokenTimeout;
            }
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException("Timeout must be a number greater than 0");
                tokenTimeout = value;
            }
        }

        /// <summary>
        /// Instantiate a new ApiConnection
        /// </summary>
        /// <param name="credentials">service credentials</param>
        public ApiConnection(Credentials credentials)
        {
            Ensure.ArgumentNotNull(credentials, "credentials");

            this.Credentials = new ApiCredentials(credentials);
            this.Connection = new Connection(Url());
            this.ConnectionISMS = new Connection(BuildBaseISMSUri());
        }

        /// <summary>
        /// Call a generic Api GET
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="request">Request object</param>
        /// <exception cref="Exception">If some error has occurred</exception>
        /// <returns>Response object wrapping data with the specified T type</returns>
        /// <seealso cref="Request"/>
        /// <seealso cref="Response"/>
        public Response Get<T>(Request request) where T : new()
        {
            try
            {
                DoAuthenticate();
                request.Headers.Authorization = Token;

                Response response;
                if (RequestIsISMS(request))
                {
                    response = ConnectionISMS.Get<T>(request);
                }
                else
                {
                    response = Connection.Get<T>(request);
                }

                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    InvalidateCredentials();
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        private bool RequestIsISMS(Request request)
        {
            return request.Endpoint.IsAbsoluteUri &&
                    request.Endpoint.AbsolutePath.Contains(ISMSUrl().AbsolutePath);
        }

        /// <summary>
        /// Call a generic Api PUT
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="request">Request object</param>
        /// <exception cref="Exception">If some error has occurred</exception>
        /// <returns>Response object wrapping data with the specified T type</returns>
        /// <seealso cref="Request"/>
        /// <seealso cref="Response"/>
        public Response Put<T>(Request request) where T : new()
        {
            try
            {
                DoAuthenticate();
                request.Headers.Authorization = Token;

                Response response;
                if (RequestIsISMS(request))
                {
                    response = ConnectionISMS.Put<T>(request);
                }
                else
                {
                    response = Connection.Put<T>(request);
                }

                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    InvalidateCredentials();
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        /// <summary>
        /// Call a generic Api POST
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="request">Request object</param>
        /// <exception cref="Exception">If some error has occurred</exception>
        /// <returns>Response object wrapping data with the specified T type</returns>
        /// <seealso cref="Request"/>
        /// <seealso cref="Response"/>
        public Response Post<T>(Request request) where T : new()
        {
            try
            {
                DoAuthenticate();
                request.Headers.Authorization = Token;

                Response response;
                if (RequestIsISMS(request)) {
                    response = ConnectionISMS.Post<T>(request);
                }
                else
                {
                    response = Connection.Post<T>(request);
                }

                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    InvalidateCredentials();
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        /// <summary>
        /// Call a generic Api DELETE
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="request">Request object</param>
        /// <exception cref="Exception">If some error has occurred</exception>
        /// <returns>Response object wrapping data with the specified T type</returns>
        /// <seealso cref="Request"/>
        /// <seealso cref="Response"/>
        public Response Delete<T>(Request request) where T : new()
        {
            try
            {
                DoAuthenticate();
                request.Headers.Authorization = Token;

                Response response;
                if (RequestIsISMS(request))
                {
                    response = ConnectionISMS.Delete<T>(request);
                }
                else
                {
                    response = Connection.Delete<T>(request);
                }

                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    InvalidateCredentials();
                }
                return response;
            }

            catch (Exception ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        /// <summary>
        /// Make a request login against the API.
        /// Remember to have valid credentials
        /// </summary>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns><c>true</c>if can authenticate successfully</returns>
        public bool Authenticate()
        {
            try
            {
                string clientId = Credentials.ClientId;
                string clientSecret = Credentials.ClientSecret;

                string username = Credentials.Username;
                string password = Credentials.Password;

                string accessKey = "access";
                string tokenKey = "token";
                string restaurantKey = "restaurant";

                dynamic body = new ExpandoObject();
                body.client_id = clientId;
                body.client_secret = clientSecret;
                if (!(string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password)))
                {
                    body.username = username;
                    body.password = password;
                }

                Request request = new Request
                {
                    Body = body,
                    Endpoint = new Uri("users/login", UriKind.Relative)
                };

                Response response =  Connection.Post<Dictionary<string, object>>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = response.Body;
                    JObject access = json[accessKey];
                    JObject push = (JObject)access["push"];
                    JObject restaurant = json[restaurantKey];
                    bool pushAvailable = Convert.ToBoolean(push["available"]);

                    if (pushAvailable)
                    {
                        ApiCredentials apiCredentials = Credentials as ApiCredentials;
                        apiCredentials.OrderAccessKey = (string)push["keyId"];
                        apiCredentials.OrderSecretKey = (string)push["keySecret"];
                        apiCredentials.RegionEndpoint = (string)push["region"];
                        apiCredentials.QueueName = (string)push["queueName"];
                    }
                    if (!(username == null && password == null))
                    {
                        RestaurantId = Convert.ToInt64(restaurant["id"]);
                    }
                    Token = (string)access[tokenKey];
                    TokenAge = DateTime.Now;

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
        /// Check if the SDK is authenticated or not
        /// </summary>
        /// <returns><c>true</c> if authenticated</returns>
        public bool IsAuthenticated()
        {
            return !string.IsNullOrEmpty(Token) && TokenAge != null;
        }

        /// <summary>
        /// Make a request login against the API optimized.
        /// Remember to have valid credentials
        /// </summary>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns><c>true</c>if can authenticate successfully</returns>
        public void DoAuthenticate()
        {
            if (string.IsNullOrEmpty(Token) || TokenAge == null)
            {
                Authenticate();
            }
            else
            {
                double timeOut = TokenTimeout;
                TimeSpan diff = DateTime.Now - (DateTime)TokenAge;
                if (diff.TotalMinutes > timeOut)
                {
                    Authenticate();
                }
            }
        }

        private void InvalidateCredentials()
        {
            Token = null;
            TokenAge = null;
        }

        private Uri Url()
        {
            string prefix = "";
            if (Credentials.Environment != Environments.PRODUCTION)
            {
                prefix = "stg-";
            }
            return new Uri($"https://{prefix}orders-api.pedidosya.com/v3");
        }

        public Uri ISMSUrl()
        {
            string url = BuildBaseISMSUrl() + "self-management/";
            return new Uri(url);
        }

        public Uri BuildBaseISMSUri()
        {
            return new Uri(BuildBaseISMSUrl());
        }
        
        public Uri IosUrl()
        {
            string prefix = "";
            if (Credentials.Environment != Environments.PRODUCTION)
            {
                prefix = "stg-";
            }
            return new Uri($"https://{prefix}management-api.pedidosya.com/integrations-order/");
        }

        private String BuildBaseISMSUrl()
        {
            string prefix = "";
            if (Credentials.Environment != Environments.PRODUCTION)
            {
                prefix = "stg-";
            }
            return $"https://{prefix}management-api.pedidosya.com/";
        }

        public String MessagesSusbcriberKey()
        {
           
            if (Credentials.Environment != Environments.PRODUCTION)
            {
                return "sub-c-3576c5a0-d334-11e9-a0bb-da9b1e19cd1e";
            }       
               return "sub-c-28a476d8-d334-11e9-a16c-569e8a5c3af3";        
        }
    }
}
