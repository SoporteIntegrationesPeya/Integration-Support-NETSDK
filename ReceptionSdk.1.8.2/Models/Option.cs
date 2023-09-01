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
    public class Option: Entity
    {
        /// <summary>
        /// The price of the option
        /// </summary>
        [DeserializeAs(Name = "price")]
        public double? Price { get; set; }

        /// <summary>
        /// The quantity of selected option
        /// </summary>
        [DeserializeAs(Name = "quantity")]
        public int? Quantity { get; set; }


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
        /// The option group of selected option
        /// </summary>
        [DeserializeAs(Name = "optionGroup")]
        public OptionGroup OptionGroup { get; set; }

        /// <summary>
        /// Default Option constructor
        /// </summary>
        public Option() : base()
        {
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }
    }
}
