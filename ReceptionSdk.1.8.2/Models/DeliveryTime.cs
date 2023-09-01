///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp;
using RestSharp.Deserializers;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The DeliveryTime class holds all the needed information about the possible delivery times for the orders
    /// </summary>
    public class DeliveryTime
    {
        /// <summary>
        /// The delivery time identification number
        /// </summary>
        [DeserializeAs(Name = "id")]
        public long Id { get; set; }

        /// <summary>
        /// The delivery time identification name
        /// </summary>
        [DeserializeAs(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The delivery time description
        /// </summary>
        [DeserializeAs(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Default DeliveryTime constructor
        /// </summary>
        public DeliveryTime() : base()
        {
        }

        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            DeliveryTime deliveryTime = (DeliveryTime)obj;
            return (this.Id == deliveryTime.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }
    }
}