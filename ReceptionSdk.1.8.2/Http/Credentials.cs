///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using System;

namespace ReceptionSdk.Http
{
    /// <summary>
    /// Service credentials for accessing API
    /// </summary>
    public class Credentials
    {
        private string clientId;
        /// <summary>
        /// The client id for service access
        /// You should ask PedidosYa for a client id
        /// </summary>
        public string ClientId
        {
            get
            {
                return clientId;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException("Client id cannot be null or empty");
                clientId = value;
            }
        }

        private string clientSecret;
        /// <summary>
        /// The client's secret access code.
        /// You should ask PedidosYa for a client secret
        /// </summary>
        public string ClientSecret
        {
            get
            {
                return clientSecret;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException("Client secret cannot be null or empty");
                clientSecret = value;
            }
        }

        /// <summary>
        /// The client's username.
        /// You should ask PedidosYa for a username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The client's password.
        /// You should ask PedidosYa for a password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The environment of the credentials
        /// </summary>
        public Environments Environment { get; set; }

        /// <summary>
        /// Instantiate a new Credentials
        /// </summary>
        public Credentials()
        {
            Environment = Environments.DEVELOPMENT;
        }

        /// <summary>
        /// Check if using centralized keys for all the restaurants or just credentials per restaurant
        /// </summary>
        /// <returns>if using centralized keys or not</returns>
        public bool CentralizedKeys()
        {
            return Username == null || Password == null;
        }
    }
}
