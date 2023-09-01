///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp;
using RestSharp.Deserializers;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The Company class holds all the needed information about the user company
    /// </summary>
    public class Company
    {
        /// <summary>
        /// The company name
        /// </summary>
        [DeserializeAs(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The company document
        /// </summary>
        [DeserializeAs(Name = "document")]
        public string Document { get; set; }

        /// <summary>
        /// Default Company constructor
        /// </summary>
        public Company() : base()
        {
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }
    }
}
