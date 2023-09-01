///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp;
using RestSharp.Deserializers;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The Address class holds all the needed information about the user address
    /// </summary>
    public class Address
    {
        /// <summary>
        /// The user address street
        /// </summary>
        [DeserializeAs(Name = "street")]
        public string Street { get; set; }

        /// <summary>
        /// The user address street corner
        /// </summary>
        [DeserializeAs(Name = "corner")]
        public string Corner { get; set; }

        /// <summary>
        /// The user address door number
        /// </summary>
        [DeserializeAs(Name = "doorNumber")]
        public string DoorNumber { get; set; }

        /// <summary>
        /// The user address complement. For example: Yellow house
        /// </summary>
        [DeserializeAs(Name = "complement")]
        public string Complement { get; set; }

        /// <summary>
        /// The user area, a.k.a: neighborhood
        /// </summary>
        [DeserializeAs(Name = "area")]
        public string Area { get; set; }

        /// <summary>
        /// The user city
        /// </summary>
        [DeserializeAs(Name = "city")]
        public string City { get; set; }

        /// <summary>
        /// The address formatted: street, corner, number, etc.
        /// For example: 'Plaza Independencia 743 esquina Ciudadela'
        /// </summary>
        [DeserializeAs(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// The address notes things such as 'the blue house', 'the ring doesn't work', etc.
        /// </summary>
        [DeserializeAs(Name = "notes")]
        public string Notes { get; set; }

        /// <summary>
        /// The user address phone
        /// </summary>
        [DeserializeAs(Name = "phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Returns the user address zip code. This fields depends on the country for example in Uruguay doesn't make much sense but in Brazil
        /// references to the user CEP
        /// </summary>
        [DeserializeAs(Name = "zipCode")]
        public string ZipCode { get; set; }

        /// <summary>
        /// The coordinates from the order
        /// </summary>
        [DeserializeAs(Name = "coordinates")]
        public string Coordinates { get; set; }

        /// <summary>
        /// Default Address constructor
        /// </summary>
        public Address() : base()
        {
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }
    }
}
