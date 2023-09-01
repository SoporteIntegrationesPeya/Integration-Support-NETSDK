///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp;
using RestSharp.Deserializers;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The RejectMessage class holds all information about possible rejection messages
    /// </summary>
    public class RejectMessage
    {
        /// <summary>
        /// The reject message identification number
        /// </summary>
        [DeserializeAs(Name = "id")]
        public long Id { get; set; }

        /// <summary>
        /// The reject message description in Spanish
        /// </summary>
        [DeserializeAs(Name = "descriptionES")]
        public string DescriptionES { get; set; }

        /// <summary>
        /// The reject message description in Portuguese
        /// </summary>
        [DeserializeAs(Name = "descriptionPT")]
        public string DescriptionPT { get; set; }

        /// <summary>
        /// Whether it's for logistics or not
        /// </summary>
        [DeserializeAs(Name = "forLogistics")]
        public bool ForLogistics { get; set; }

        /// <summary>
        /// Whether it's for pickup or not
        /// </summary>
        [DeserializeAs(Name = "forPickup")]
        public bool ForPickup { get; set; }

        /// <summary>
        /// Default RejectMessage constructor
        /// </summary>
        public RejectMessage() : base()
        {
        }

        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            RejectMessage deliveryTime = (RejectMessage)obj;
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