///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp.Deserializers;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The Portal class holds all the information about the order platform.
    /// </summary>
    public class Portal
    {
        /// <summary>
        /// The portal identification number
        /// </summary>
        [DeserializeAs(Name = "id")]
        public long Id { get; set; }

        /// <summary>
        /// The portal name. For example: 'Pedidos Ya', 'Pizza Pizza'
        /// </summary>
        [DeserializeAs(Name = "name")]
        public string Name { get; set; }
    }
}
