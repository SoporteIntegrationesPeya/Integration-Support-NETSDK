///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp;
using RestSharp.Deserializers;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The Discount class holds all the needed information about the order's discounts
    /// </summary>
    public class Discount
    {
        /// <summary>
        /// The discount net amount
        /// </summary>
        [DeserializeAs(Name = "amount")]
        public double Amount { get; set; }

        /// <summary>
        /// The discount original value
        /// </summary>
        [DeserializeAs(Name = "value")]
        public double Value { get; set; }

        /// <summary>
        /// The discount type
        /// </summary>
        [DeserializeAs(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// The discount priority
        /// </summary>
        [DeserializeAs(Name = "priority")]
        public string Priority { get; set; }

        /// <summary>
        /// The discount notes
        /// </summary>
        [DeserializeAs(Name = "notes")]
        public string Notes { get; set; }

        /// <summary>
        /// The discount code
        /// </summary>
        [DeserializeAs(Name = "code")]
        public string Code { get; set; }

        /// <summary>
        /// Default Discount constructor
        /// </summary>
        public Discount() : base()
        {
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }

    }
}
