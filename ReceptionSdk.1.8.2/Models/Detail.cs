///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp;
using RestSharp.Deserializers;
using System.Collections.Generic;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The Detail class holds all the needed information about the order details
    /// </summary>
    public class Detail
    {
        /// <summary>
        /// The notes included for the product. For example: 'The burger without tomato please.'
        /// </summary>
        [DeserializeAs(Name = "notes")]
        public string Notes { get; set; }

        /// <summary>
        /// The product options. For example: 'Ketchup', 'Bacon'
        /// </summary>
        [DeserializeAs(Name = "optionGroups")]
        public List<OrderOptionGroup> OptionGroups { get; set; } = new List<OrderOptionGroup>();

        /// <summary>
        /// The associated product
        /// </summary>
        [DeserializeAs(Name = "product")]
        public Product Product { get; set; }

        /// <summary>
        /// The amount of products
        /// </summary>
        [DeserializeAs(Name = "quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// The product subtotal
        /// </summary>
        [DeserializeAs(Name = "subtotal")]
        public double Subtotal { get; set; }

        /// <summary>
        /// The product unit price
        /// </summary>
        [DeserializeAs(Name = "unitPrice")]
        public double UnitPrice { get; set; }

        /// <summary>
        /// The discount amount of this detail
        /// </summary>
        [DeserializeAs(Name = "discount")]
        public double Discount { get; set; }

        /// <summary>
        /// The total amount of this details
        /// </summary>
        [DeserializeAs(Name = "total")]
        public double Total { get; set; }

        /// <summary>
        /// Default Detail constructor
        /// </summary>
        public Detail() : base()
        {
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }
    }
}