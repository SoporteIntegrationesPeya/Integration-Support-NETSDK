using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReceptionSdk.Enums;
using ReceptionSdk.Helpers;
using RestSharp.Deserializers;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The Reconciliation class holds all the reconciliation data.
    /// </summary>
    public class Reconciliation
    {
        /// <summary>
        /// The order id. 
        /// </summary>
        [DeserializeAs(Name = "id")]
        [JsonProperty(PropertyName = "id")]
        public long OrderId { get; set; }
        
        /// <summary>
        /// The total gross of the order. 
        /// </summary>
        [DeserializeAs(Name = "totalGross")]
        [JsonProperty(PropertyName = "totalGross")]
        public double TotalGross { get; set; }
        
        /// <summary>
        /// The list of order's products to reconcile. 
        /// </summary>
        [DeserializeAs(Name = "products")]
        [JsonProperty(PropertyName = "products")]
        private List<ProductsReconciliation> Products { get; set; }

        public Reconciliation()
        {
            Products = new List<ProductsReconciliation>();
        }

        /// <summary>
        /// Method that allows reconcile an order by the concept of eliminating a product. 
        /// </summary>
        public void RemovalModificationProductBuilder(String integrationCode)
        {
            Ensure.ArgumentNotNull(integrationCode, "integrationCode");
            ProductsReconciliation productsReconciliation = new ProductsReconciliation
            {
                IntegrationCode = integrationCode,
                ModificationType = OrderProductModificationType.REMOVAL
            };
            Products.Add(productsReconciliation);
        }
        
        /// <summary>
        /// Method that allows reconcile an order by the concept of replacing a product.
        /// </summary>
        public void ReplacementModificationProductBuilder(String integrationCode, String oldIntegrationCode, int quantity, double unitPrice)
        {
            ValidateCommonParametersToModifyProduct(integrationCode, quantity, unitPrice);
            Ensure.ArgumentNotNull(oldIntegrationCode, "oldIntegrationCode");
            ProductsReconciliation productsReconciliation = new ProductsReconciliation
            {
                IntegrationCode = integrationCode,
                OldIntegrationCode = oldIntegrationCode,
                Quantity = quantity,
                ModificationType = OrderProductModificationType.REPLACEMENT,
                UnitPrice = unitPrice
            };
            Products.Add(productsReconciliation);
        }
        
        /// <summary>
        /// Method that allows reconcile an order by the concept of adding a product.
        /// </summary>
        public void AdditionModificationProductBuilder(String integrationCode, int quantity, double unitPrice)
        {
            ValidateCommonParametersToModifyProduct(integrationCode, quantity, unitPrice);
            
            ProductsReconciliation productsReconciliation = new ProductsReconciliation
            {
                IntegrationCode = integrationCode,
                Quantity = quantity,
                ModificationType = OrderProductModificationType.ADDITION,
                UnitPrice = unitPrice
            };
            Products.Add(productsReconciliation);
        }
        
        /// <summary>
        /// Method that allows reconcile an order by the concept of changing a product.
        /// </summary>
        public void ChangeModificationProductBuilder(String integrationCode, int quantity, double unitPrice)
        {
            ValidateCommonParametersToModifyProduct(integrationCode, quantity, unitPrice);

            ProductsReconciliation productsReconciliation = new ProductsReconciliation
            {
                IntegrationCode = integrationCode,
                Quantity = quantity,
                ModificationType = OrderProductModificationType.CHANGE,
                UnitPrice = unitPrice
            };
            Products.Add(productsReconciliation);
        }

        private static void ValidateCommonParametersToModifyProduct(string integrationCode, int quantity, double unitPrice)
        {
            Ensure.ArgumentNotNull(integrationCode, "integrationCode");
            Ensure.GreaterThanZero(quantity, "quantity");
            Ensure.GreaterThanZero((long) unitPrice, "unitPrice");
        }
    }

    public class ProductsReconciliation
    {
        [DeserializeAs(Name = "integrationCode")]
        [JsonProperty(PropertyName = "integrationCode")]
        public string IntegrationCode { get; set; }
        
        [DeserializeAs(Name = "oldIntegrationCode")]
        [JsonProperty(PropertyName = "oldIntegrationCode", NullValueHandling = NullValueHandling.Ignore)]
//        [JsonIgnore]
        public string OldIntegrationCode { get; set; }
        
        [DeserializeAs(Name = "quantity")]
        [JsonProperty(PropertyName = "quantity", NullValueHandling = NullValueHandling.Ignore)]
        public int? Quantity { get; set; }
        
        [DeserializeAs(Name = "unitPrice")]
        [JsonProperty(PropertyName = "unitPrice", NullValueHandling = NullValueHandling.Ignore)]
        public double? UnitPrice  { get; set; }
        
        [DeserializeAs(Name = "modificationType")]
        [JsonProperty(PropertyName = "modification")]
        public OrderProductModificationType  ModificationType { get; set; }
    }
}