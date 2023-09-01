///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp;
using RestSharp.Deserializers;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The Driver class holds all the needed information about the order tracking's driver
    /// </summary>
    public class Driver
    {
        /// <summary>
        /// The driver's name
        /// </summary>
        [DeserializeAs(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The driver's location
        /// </summary>
        [DeserializeAs(Name = "location")]
        public Location Location { get; set; }

        /// <summary>
        /// Default Driver constructor
        /// </summary>
        public Driver() : base()
        {
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }

    }
}
