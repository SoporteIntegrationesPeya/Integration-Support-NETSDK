///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
namespace ReceptionSdk.Http
{
    /// <summary>
    /// Internal service credentials for accessing API
    /// </summary>
    class ApiCredentials : Credentials
    {
        /// <summary>
        /// Push method access key
        /// </summary>
        public string OrderAccessKey { get; set; }

        /// <summary>
        /// Push method secret key
        /// </summary>
        public string OrderSecretKey { get; set; }

        /// <summary>
        /// Push method environment
        /// </summary>
        public string RegionEndpoint { get; set; }

        /// <summary>
        /// Push method orders queue
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// Instantiate a new ApiCredentials
        /// <param name="credentials">service credentials</param>
        /// </summary>
        public ApiCredentials(Credentials credentials)
        {
            ClientId = credentials.ClientId;
            ClientSecret = credentials.ClientSecret;
            Username = credentials.Username;
            Password = credentials.Password;
            Environment = credentials.Environment;
        }

        /// <summary>
        /// Check if the credentials are allowed to use the push receiving method
        /// </summary>
        /// <returns></returns>
        public bool PushAvailable()
        {
            return !(string.IsNullOrEmpty(OrderAccessKey) ||
                string.IsNullOrEmpty(OrderSecretKey) ||
                string.IsNullOrEmpty(RegionEndpoint) ||
                string.IsNullOrEmpty(QueueName));
        }
    }
}
