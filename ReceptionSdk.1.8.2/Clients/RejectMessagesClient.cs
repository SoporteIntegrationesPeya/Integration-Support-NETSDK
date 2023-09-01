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
    /// A client for Reject Messages API.
    /// </summary>
    public class RejectMessagesClient
    {
        private ApiConnection Connection { get; set; }

        /// <summary>
        /// Instantiate a new Reject Messages API client.
        /// </summary>
        /// <param name="connection">an API connection</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        public RejectMessagesClient(ApiConnection connection)
        {
            Ensure.ArgumentNotNull(connection, "connection");

            Connection = connection;
        }

        /// <summary>
        /// Returns all the possible reject messages
        /// </summary>
        /// <exception cref="ApiException">if some error has occurred</exception>
        /// <returns>the list of reject messages</returns>
        /// <seealso cref="RejectMessage"/>
        public IList<RejectMessage> GetAll()
        {
            try
            {
                Request request = new Request();
                request.Endpoint = new Uri("rejectMessages", UriKind.Relative);
                List<RejectMessage> rejectMessages = new List<RejectMessage>();
                Response response = Connection.Get<RejectMessagesResponse>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (response.Body.RejectMessages.Count == response.Body.Total)
                    {
                        return response.Body.RejectMessages;
                    }
                    else
                    {
                        rejectMessages.AddRange(response.Body.RejectMessages);
                        while (response.Body.RejectMessages != null && response.Body.RejectMessages.Count != 0)
                        {
                            request.Endpoint = new Uri("rejectMessages", UriKind.Relative);
                            request.Parameters.offset = rejectMessages.Count;
                            response = Connection.Get<RejectMessagesResponse>(request);
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                rejectMessages.AddRange(response.Body.RejectMessages);
                            }
                            else
                            {
                                throw new ApiException(response);
                            }
                        }
                        return rejectMessages;
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
