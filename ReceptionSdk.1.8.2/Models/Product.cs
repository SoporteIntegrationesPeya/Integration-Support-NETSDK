///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using ReceptionSdk.Helpers;
using RestSharp;
using RestSharp.Deserializers;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The Product class holds all the product data.
    /// </summary>
    public class Product: Entity
    {
        /// <summary>
        /// The product description. For example: '200 gr. ground beef burger with french fries.'
        /// </summary>
        [DeserializeAs(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// The product section
        /// </summary>
        [DeserializeAs(Name = "section")]
        public Section Section { get; set; }

        /// <summary>
        /// The product's image id
        /// </summary>
        [DeserializeAs(Name = "image")]
        public string Image { get; set; }

        /// <summary>
        /// The product price.
        /// </summary>
        [DeserializeAs(Name = "price")]
        public double? Price { get; set; }

        /// <summary>
        /// The product gtin.
        /// </summary>
        [DeserializeAs(Name = "gtin")]
        public string Gtin { get; set; }

        /// <summary>
        /// The product requiresAgeCheck.
        /// </summary>
        [DeserializeAs(Name = "requiresAgeCheck")]
        public bool? RequiresAgeCheck { get; set; }

        /// <summary>
        /// The product measurementUnit.
        /// </summary>
        [DeserializeAs(Name = "measurementUnit")]
        public string MeasurementUnit { get; set; }

        /// <summary>
        /// The product contentQuantity.
        /// </summary>
        [DeserializeAs(Name = "contentQuantity")]
        public double? ContentQuantity { get; set; }

        /// <summary>
        /// The product prescriptionBehaviour.
        /// </summary>
        [DeserializeAs(Name = "prescriptionBehaviour")]
        public string PrescriptionBehaviour { get; set; }

        /// <summary>
        /// Default Product constructor
        /// </summary>
        public Product() : base()
        {
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }
    }
}
