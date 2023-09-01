///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp;
using RestSharp.Deserializers;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The User class holds all the user data. The user address, the client email, the user full name, etc.
    /// </summary>
    public class User
    {
        /// <summary>
        /// The user email
        /// </summary>
        [DeserializeAs(Name = "email")]
        public string Email { get; set; }

        /// <summary>
        /// The user identity card. For example, the Uruguayan CI number, the Brazilian CNPF or CNPJ number, etc.
        /// </summary>
        [DeserializeAs(Name = "identityCard")]
        public string IdentityCard { get; set; }

        /// <summary>
        /// <c>true</c> if the user is new
        /// </summary>
        [DeserializeAs(Name = "isNew")]
        public bool IsNew { get; set; }

        /// <summary>
        /// The user last name
        /// </summary>
        [DeserializeAs(Name = "lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// The user name
        /// </summary>
        [DeserializeAs(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The amount of orders that the user has on the specified platform
        /// </summary>
        [DeserializeAs(Name = "orderCount")]
        public int OrderCount { get; set; }

        /// <summary>
        /// The platform that the user belongs to
        /// </summary>
        [DeserializeAs(Name = "platform")]
        public string Platform { get; set; }

        /// <summary>
        /// The user type
        /// </summary>
        [DeserializeAs(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// The user company
        /// </summary>
        [DeserializeAs(Name = "company")]
        public Company Company { get; set; }

        /// <summary>
        /// Default User constructor
        /// </summary>
        public User() : base()
        {
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }
    }
}