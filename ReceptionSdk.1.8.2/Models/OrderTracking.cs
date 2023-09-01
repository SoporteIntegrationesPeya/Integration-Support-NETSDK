///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp;
using RestSharp.Deserializers;
using System;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The OrderTracking class holds all the needed information about the order's tracking
    /// </summary>
    public class OrderTracking
    {
        /// <summary>
        /// The tracking's state
        /// Possible values are inside TrackingState class
        /// </summary>
        [DeserializeAs(Name = "state")]
        public string State { get; set; }

        /// <summary>
        /// The tracking's driver
        /// </summary>
        [DeserializeAs(Name = "driver")]
        public Driver Driver { get; set; }

        /// <summary>
        /// The tracking's pickup date
        /// </summary>
        [DeserializeAs(Name = "pickupDate")]
        public DateTime PickupDate { get; set; }

        /// <summary>
        /// The tracking's estimated delivery date
        /// </summary>
        [DeserializeAs(Name = "estimatedDeliveryDate")]
        public DateTime EstimatedDeliveryDate { get; set; }

        /// <summary>
        /// Default OrderTracking constructor
        /// </summary>
        public OrderTracking() : base()
        {
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }

    }
}
