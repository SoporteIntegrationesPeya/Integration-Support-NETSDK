///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp;
using RestSharp.Deserializers;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The Location class holds all the needed information about the driver's location
    /// </summary>
    public class Location
    {
        /// <summary>
        /// The location's latitude
        /// </summary>
        [DeserializeAs(Name = "lat")]
        public double Lat { get; set; }

        /// <summary>
        /// The location's longitude
        /// </summary>
        [DeserializeAs(Name = "lng")]
        public double Lng { get; set; }

        /// <summary>
        /// Default Location constructor
        /// </summary>
        public Location() : base()
        {
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }

    }
}
