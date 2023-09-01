///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using System.Net;

namespace ReceptionSdk.Http
{
    /// <summary>
    /// API response data class
    /// </summary>
    public class Response
    {

        /// <summary>
        /// HTTP status code
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        
        /// <summary>
        /// Object representation of the body of the response
        /// </summary>
        public dynamic Body { get; set; }

        /// <summary>
        /// Raw body response
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Instantiate a new Response
        /// </summary>
        public Response()
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }
    }
}
