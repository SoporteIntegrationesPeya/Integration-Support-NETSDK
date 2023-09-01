///
/// Copyright (C) 2019 PedidosYa - All Rights Reserved
///
using RestSharp.Deserializers;
using RestSharp;


namespace ReceptionSdk.Models
{
    /// <summary>
    /// The Message class holds all the needed information about the message received
    /// </summary>
    public class Message
    {
        /// <summary>
        /// The message title
        /// </summary>
        [DeserializeAs(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// The message description
        /// </summary>
        [DeserializeAs(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// The message type
        /// </summary>
        [DeserializeAs(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// The message image url
        /// </summary>
        [DeserializeAs(Name = "imageUrl")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// The message restaurant
        /// </summary>
        [DeserializeAs(Name = "restaurant")]
        public Restaurant Restaurant { get; set; }

        /// <summary>
        /// Default Message constructor
        /// </summary>
        public Message() : base()
        {
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }

    }
}
