///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp;
using RestSharp.Deserializers;
using System.Collections.Generic;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The OptionGroup class holds all the product options
    /// </summary>
    public class OptionGroup: Entity
    {
        /// <summary>
        /// The product reference
        /// </summary>
        [DeserializeAs(Name = "product")]
        public Product Product { get; set; }

        /// <summary>
        /// The maximum quantity options selected
        /// </summary>
        [DeserializeAs(Name = "maximumQuantity")]
        public int? MaximumQuantity { get; set; }

        /// <summary>
        /// The minimum quantity options selected
        /// </summary>
        [DeserializeAs(Name = "minumumQuantity")]
        public int? MinumumQuantity { get; set; }

        /// <summary>
        /// Default OptionGroup constructor
        /// </summary>
        public OptionGroup() : base()
        {
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }
    }
}