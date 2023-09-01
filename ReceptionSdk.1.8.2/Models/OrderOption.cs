///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp;
using RestSharp.Deserializers;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The Option class holds all the needed information about the product option
    /// </summary>
    public class OrderOption : Entity
    {
        /// <summary>
        /// The Amount of the option
        /// </summary>
        [DeserializeAs(Name = "amount")]
        public double? Amount { get; set; }

        /// <summary>
        /// The quantity of selected option
        /// </summary>
        [DeserializeAs(Name = "quantity")]
        public int? Quantity { get; set; }

        /// <summary>
        /// The Stock of selected option
        /// </summary>
        [DeserializeAs(Name = "stock")]
        public int? Stock { get; set; }

        /// <summary>
        /// The Gtin of selected option
        /// </summary>
        [DeserializeAs(Name = "gtin")]
        public string Gtin { get; set; }


        /// <summary>
        /// <c>true</c> if the option modifies the price of the option
        /// </summary>
        [DeserializeAs(Name = "modifiesPrice")]
        public bool? ModifiesPrice { get; set; }

        /// <summary>
        /// <c>true</c> if the option requires age check for the option
        /// </summary>
        [DeserializeAs(Name = "requiresAgeCheck")]
        public bool? RequiresAgeCheck { get; set; }

        /// <summary>
        /// Default Option constructor
        /// </summary>
        public OrderOption() : base()
        {
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }
    }
}
