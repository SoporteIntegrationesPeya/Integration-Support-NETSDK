///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp;
using RestSharp.Deserializers;
using System.Collections.Generic;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The OrderOptionGroup class holds all the needed information about the order product option group
    /// </summary>
    public class OrderOptionGroup: Entity
    {
        /// <summary>
        /// The maximum quantity options selected
        /// </summary>
        [DeserializeAs(Name = "maximumQuantity")]
        public int MaximumQuantity { get; set; }

        /// <summary>
        /// The minimum quantity options selected
        /// </summary>
        [DeserializeAs(Name = "minumumQuantity")]
        public int MinumumQuantity { get; set; }

        /// <summary>
        /// The options. For example: 'Ketchup', 'Bacon'
        /// </summary>
        [DeserializeAs(Name = "options")]
        public List<OrderOption> Options { get; set; } = new List<OrderOption>();

        /// <summary>
        /// Default OrderOptionGroup constructor
        /// </summary>
        public OrderOptionGroup() : base()
        {
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }
    }
}