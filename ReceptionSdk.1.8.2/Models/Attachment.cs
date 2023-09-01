///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp;
using RestSharp.Deserializers;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The Attachment class holds all the needed information about the order attachment
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// The attachment url
        /// </summary>
        [DeserializeAs(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Default Attachment constructor
        /// </summary>
        public Attachment() : base()
        {
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }
    }
}
