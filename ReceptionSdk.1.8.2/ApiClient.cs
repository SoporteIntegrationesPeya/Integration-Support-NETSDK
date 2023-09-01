///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using ReceptionSdk.Http;
using ReceptionSdk.Clients;
using ReceptionSdk.Helpers;
using System.Net;

namespace ReceptionSdk
{
    /// <summary>
    /// A client for the PedidosYa API
    /// </summary>
    public class ApiClient
    {
        /// <summary>
        /// Provides a client connection to make rest requests to HTTP endpoints.
        /// </summary>
        public ApiConnection Connection { get; private set; }

        /// <summary>
        /// Client for the Orders API.
        /// </summary>
        public OrdersClient Order { get; private set; }

        /// <summary>
        /// Client for the Products API
        /// </summary>
        public ProductsClient Product { get; private set; }

        /// <summary>
        /// Client for the Option Groups API
        /// </summary>
        public OptionGroupClient OptionGroup { get; private set; }

        /// <summary>
        /// Client for the Options API
        /// </summary>
        public OptionsClient Option { get; private set; }

        /// <summary>
        /// Client for the Sections API.
        /// </summary>
        public SectionsClient Section { get; private set; }

        /// <summary>
        /// Client for the Events API.
        /// </summary>
        public EventsClient Event { get; private set; }

        /// <summary>
        /// Client for the Restaurants API.
        /// </summary>
        public RestaurantsClient Restaurant { get; private set; }

        /// <summary>
        /// Client for the Messages API.
        /// </summary>
        public MessagesClient Message { get; private set; }
        
        public PromotionClient Promotion { get; private set; }

        /// <summary>
        /// Instantiate a new API client.
        /// </summary>
        /// <param name="credentials">the API user credentials</param>
        public ApiClient(Credentials credentials)
        {
            Ensure.ArgumentNotNull(credentials, "credentials");

            Connection = new ApiConnection(credentials);
            Order = new OrdersClient(Connection);
            Event = new EventsClient(Connection);
            Restaurant = new RestaurantsClient(Connection);
            Message = new MessagesClient(Connection);
            Product = new ProductsClient(Connection);
            OptionGroup = new OptionGroupClient(Connection);
            Option = new OptionsClient(Connection);
            Section = new SectionsClient(Connection);
            Promotion =new PromotionClient(Connection);
        }
    }
}
