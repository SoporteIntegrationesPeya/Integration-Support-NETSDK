///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp;
using RestSharp.Deserializers;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The Restaurnat class holds all the restaurant information.
    /// </summary>
    public class Restaurant
    {
        /// <summary>
        /// The restaurant identification number
        /// </summary>
        [DeserializeAs(Name = "id")]
        public long Id { get; set; }

        /// <summary>
        /// The restaurant name
        /// </summary>
        [DeserializeAs(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The restaurant integration code
        /// </summary>
        [DeserializeAs(Name = "integrationCode")]
        public string IntegrationCode { get; set; }

        /// <summary>
        /// The restaurant integration code
        /// </summary>
        [DeserializeAs(Name = "integrationName")]
        public string IntegrationName { get; set; }

        /// <summary>
        /// Default Restaurant constructor
        /// </summary>
        public Restaurant() : base()
        {
        }

        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            Restaurant restaurant = (Restaurant) obj;
            return (Id == restaurant.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }
    }
}
