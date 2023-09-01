///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp;
using RestSharp.Deserializers;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The Section class holds all the information about the section of a product.
    /// </summary>
    public class Section: Entity
    {
        /// <summary>
        /// Default Section constructor
        /// </summary>
        public Section() : base()
        {
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }
    }
}
