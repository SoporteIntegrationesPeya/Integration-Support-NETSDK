///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReceptionSdk.Exceptions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ReceptionSdk.Http
{
    /// <summary>
    /// HTTP generic REST client
    /// </summary>
    public class Connection
    {
        private RestClient httpClient = new RestClient();
        //WebRequest webRequest = WebRequest.Create();
        private const string CONTENT_TYPE = "application/json;charset=utf-8";
        private RestClient HttpClient
        {
            get { return httpClient; }
        }
        /// <summary>
        /// The service base url.
        /// URL format is: 'http://example.com/service'
        /// </summary>
        public Uri BaseUrl { get; set; }

        /// <summary>
        /// Instantiate a new Connection
        /// </summary>
        /// <param name="baseUrl"></param>
        public Connection(Uri baseUrl)
        {
            HttpClient.BaseUrl = baseUrl;
            this.BaseUrl = baseUrl;
        }

        /// <summary>
        /// Call a generic HTTP GET
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="request">Request object</param>
        /// <exception cref="ConnectionException">If some error has occurred</exception>
        /// <returns>Response object wrapping data with the specified T type</returns>
        /// <seealso cref="Request"/>
        /// <seealso cref="Response"/>
        public Response Get<T>(Request request) where T : new()
        {
            try
            {
                IRestRequest req = new RestRequest(request.Endpoint, Method.GET)
                {
                    Timeout = (int)request.Timeout.TotalMilliseconds,
                    RequestFormat = DataFormat.Json,
                    OnBeforeDeserialization = onResponse => { onResponse.ContentType = "application/json"; }
                };

                // Add URL query string parameters
                foreach (KeyValuePair<string, object> param in request.Parameters)
                {
                    req.AddQueryParameter(param.Key, Convert.ToString(param.Value));
                }

                // Add request headers
                req.AddHeader("Content-Type", CONTENT_TYPE);
                foreach (KeyValuePair<string, object> param in request.Headers)
                {
                    req.AddHeader(param.Key, Convert.ToString(param.Value));
                }

                // Execute the call
                IRestResponse<T> res = httpClient.Execute<T>(req);

                // Build and return the response
                Response response = new Response
                {
                    Content = res.Content,
                    Body = res.Data,
                    StatusCode = res.StatusCode
                };
                if (res.ErrorException != null)
                {
                    throw new ConnectionException("Cannot execute operation", res.ErrorException);
                }
                return response;
            }
            catch (ConnectionException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ConnectionException("Cannot execute operation, an unknown error has occurred", ex);
            }
        }

        /// <summary>
        /// Call a generic HTTP POST
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="request">Request object</param>
        /// <exception cref="ConnectionException">If some error has occurred</exception>
        /// <returns>Response object wrapping data with the specified T type</returns>
        /// <seealso cref="Request"/>
        /// <seealso cref="Response"/>
        public Response Post<T>(Request request) where T : new()
        {
            try
            {
                Uri webAddress = BuildAddress(request);
                HttpWebRequest reqWReques = BuildRequest(webAddress, request, Method.POST);
                Response response = BuildResponse(reqWReques);
                return response;
            }
            catch (ConnectionException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ConnectionException("Cannot execute operation, an unknown error has occurred", ex);
            }
        }

        /// <summary>
        /// Call a generic HTTP PUT
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="request">Request object</param>
        /// <exception cref="ConnectionException">If some error has occurred</exception>
        /// <returns>Response object wrapping data with the specified T type</returns>
        /// <seealso cref="Request"/>
        /// <seealso cref="Response"/>
        public Response Put<T>(Request request) where T : new()
        {
            try
            {
                Uri webAddress = BuildAddress(request);
                HttpWebRequest reqWReques = BuildRequest(webAddress, request, Method.PUT);
                Response response = BuildResponse(reqWReques);
                return response;
            }
            catch (ConnectionException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ConnectionException("Cannot execute operation, an unknown error has occurred", ex);
            }
        }

        /// <summary>
        /// Call a generic HTTP DELETE
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="request">Request object</param>
        /// <exception cref="ConnectionException">If some error has occurred</exception>
        /// <returns>Response object wrapping data with the specified T type</returns>
        /// <seealso cref="Request"/>
        /// <seealso cref="Response"/>
        public Response Delete<T>(Request request) where T : new()
        {
            try
            {
                Uri webAddress = BuildAddress(request);
                HttpWebRequest reqWReques = BuildRequest(webAddress, request, Method.DELETE);
                Response response = BuildResponse(reqWReques);
                return response;
            }
            catch (ConnectionException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ConnectionException("Cannot execute operation, an unknown error has occurred", ex);
            }
        }

        private Uri BuildAddress(Request request)
        {
            StringBuilder webstring = new StringBuilder(request.Endpoint.ToString());

            int index = 1;
            foreach (KeyValuePair<string, object> param in request.Parameters)
            {
                if (index == 1)
                {
                    webstring.Append("?");
                    webstring.Append(param.Key);
                    webstring.Append("=");
                    webstring.Append(Convert.ToString(param.Value));
                }
                else
                {
                    webstring.Append("&");
                    webstring.Append(param.Key);
                    webstring.Append("=");
                    webstring.Append(Convert.ToString(param.Value));
                }
                index++;
            }

            String address;
            if (request.Endpoint.IsAbsoluteUri)
            {
                address = request.Endpoint.ToString();
            }
            else
            {
                address = this.BaseUrl + "/" + request.Endpoint.ToString();
            }
            return new Uri(address, UriKind.Absolute); ;
        }
        private HttpWebRequest BuildRequest(Uri webAddress, Request request, Method method)
        {
            // Create a request using a URL that can receive a post.
            HttpWebRequest reqWRequest = (HttpWebRequest)WebRequest.Create(webAddress);

            // Set the Method property of the request to POST.
            reqWRequest.Method = method.ToString();
            reqWRequest.Timeout = (int)request.Timeout.TotalMilliseconds;

            // Add request headers
            reqWRequest.ContentType = CONTENT_TYPE;
            foreach (KeyValuePair<string, object> param in request.Headers)
            {
                reqWRequest.Headers.Add(param.Key, Convert.ToString(param.Value));
            }

            Stream dataStream;
            // Add request body
            if (request.Body != null)
            {
                // Create POST data and convert it to a byte array.
                string requestBody = JsonConvert.SerializeObject(request.Body);
                byte[] byteArray = Encoding.UTF8.GetBytes(requestBody);

                // Set the ContentLength property of the WebRequest.
                reqWRequest.ContentLength = byteArray.Length;

                // Get the request stream.
                dataStream = reqWRequest.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();
            }
            return reqWRequest;
        }

        private Response BuildResponse(HttpWebRequest request)
        {
            // Build and return the response
            Response response = new Response();

            // Get the response.
            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)request.GetResponse();
                FillResponse(response, res);
                if (res.StatusCode != HttpStatusCode.OK && res.StatusCode != HttpStatusCode.NoContent)
                {
                    throw new ConnectionException("Cannot execute operation");
                }
            }
            catch(WebException we)
            {
                if (we.Status == WebExceptionStatus.ProtocolError)
                {
                    if (((HttpWebResponse)we.Response).StatusCode != HttpStatusCode.OK)
                    {
                        HttpWebResponse wResponse = (HttpWebResponse)we.Response;
                        FillResponse(response, wResponse);
                        return response;
                    }
                }
                else
                {
                    throw new ConnectionException("Cannot execute operation");
                }
            }
            return response;
        }
        private void FillResponse(Response response, HttpWebResponse res)
        {
            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            Stream dataStream;
            using (dataStream = res.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();

                response.Content = responseFromServer;

                // Converts the contento Json to an Jsobjecton
                response.Body = JsonConvert.DeserializeObject<object>(responseFromServer);
            }
            response.StatusCode = res.StatusCode;
        }
    }
}
