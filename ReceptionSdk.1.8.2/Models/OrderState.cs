///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
namespace ReceptionSdk.Models
{
    public class OrderState
    {
        /// <summary>
        /// State of the order when is new and ready to be answered
        /// </summary>
        public const string PENDING = "PENDING";

        /// <summary>
        /// State of the order when is confirmed
        /// </summary>
        public const string CONFIRMED = "CONFIRMED";

        /// <summary>
        /// State of the order when is rejected
        /// </summary>
        public const string REJECTED = "REJECTED";
    }
}