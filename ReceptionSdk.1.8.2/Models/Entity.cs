///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Deserializers;

namespace ReceptionSdk.Models
{
    public class Entity
    {
        /// <summary>
        /// The Entity name
        /// </summary>
        [DeserializeAs(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The entity index
        /// </summary>
        [DeserializeAs(Name = "index")]
        public int? Index { get; set; }

        //// <summary>
        /// The entity external integration code
        /// </summary>
        [DeserializeAs(Name = "integrationCode")]
        public string IntegrationCode { get; set; }

        /// <summary>
        /// The entity external integration name
        /// </summary>
        public string IntegrationName { get; set; }

        /// <summary>
        /// The entity status
        /// </summary>
        public bool? Enabled { get; set; }

        public Entity() : base()
        {
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }
    }
}
