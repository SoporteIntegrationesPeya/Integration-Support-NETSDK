///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using System;
using System.Dynamic;

namespace ReceptionSdk.Http
{
    /// <summary>
    /// API request data class
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Body of the request represented as a object
        /// </summary>
        public dynamic Body { get; set; }

        /// <summary>
        /// Dictionary of needed headers for the request
        /// </summary>
        public dynamic Headers { get; set; }

        /// <summary>
        /// Dictionary of needed parameters for the request
        /// </summary>
        public dynamic Parameters { get; private set; }

        /// <summary>
        /// Requests endpoint. Should be relative
        /// <see cref="Uri"/>
        /// <seealso cref="UriKind"/>
        /// </summary>
        public Uri Endpoint { get; set; }

        /// <summary>
        /// Defined timeout of the request
        /// <see cref="TimeSpan"/>
        /// </summary>
        public TimeSpan Timeout { get; set; }

        /// <summary>
        /// Instantiate a new Request
        /// </summary>
        public Request()
        {
            Headers = new ExpandoObject();
            Parameters = new ExpandoObject();
            Timeout = TimeSpan.FromSeconds(10);
        }
    }
}
