///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp;
using RestSharp.Deserializers;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The Card class holds all the needed information about the payment's card
    /// </summary>
    public class Card
    {
        /// <summary>
        /// The card brand
        /// </summary>
        [DeserializeAs(Name = "brand")]
        public string Brand { get; set; }

        /// <summary>
        /// The card operation type
        /// </summary>
        [DeserializeAs(Name = "operationType")]
        public string OperationType { get; set; }

        /// <summary>
        /// The card issuer
        /// </summary>
        [DeserializeAs(Name = "issuer")]
        public string Issuer { get; set; }

        /// <summary>
        /// Default Card constructor
        /// </summary>
        public Card() : base()
        {
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }

    }
}
