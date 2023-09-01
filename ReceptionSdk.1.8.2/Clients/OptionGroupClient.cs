using ReceptionSdk.Http;
using ReceptionSdk.Models;
using System;
using System.Dynamic;

namespace ReceptionSdk.Clients
{
    /// <summary>
    /// A client for Option Groups API.
    /// </summary>
    public class OptionGroupClient : MenusClient
    {
        private ApiConnection Connection { get; set; }

        /// <summary>
        /// Instantiate a new Option Groups API client.
        /// </summary>
        /// <param name="connection">an API connection</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        public OptionGroupClient(ApiConnection connection) : base(connection) { }

        public override ExpandoObject GetItemPayload(object menuItem, long restaurantId)
        {
            return OptionGroupPayload((OptionGroup)menuItem, restaurantId);
        }

        private ExpandoObject OptionGroupPayload(OptionGroup optionGroup, long restaurantId)
        {
            dynamic body = new ExpandoObject();
            body.product = new ExpandoObject();
            body.product.partnerId = restaurantId;

            if (optionGroup.Product != null)
            {
                if (optionGroup.Product.IntegrationCode != null)
                {
                    body.product.integrationCode = optionGroup.Product.IntegrationCode;
                }
                    
            }
            if (optionGroup.Name != null)
            {
                body.name = optionGroup.Name;
            }
            if (optionGroup.IntegrationCode != null)
            {
                body.integrationCode = optionGroup.IntegrationCode;
            }
            if (optionGroup.IntegrationName != null)
            {
                body.integrationName = optionGroup.IntegrationName;
            }
            if (optionGroup.MaximumQuantity != null)
            {
                body.maximumQuantity = optionGroup.MaximumQuantity;
            }
            if (optionGroup.MinumumQuantity != null)
            {
                body.minimumQuantity = optionGroup.MinumumQuantity;
            }
            if (optionGroup.Index != null)
            {
                body.index = optionGroup.Index;
            }
            return body;
        }

        public override string GetItemRoute()
        {
            return "optionGroup";
        }

        public override string GetItemsRoute(object menuItem)
        {
            return $"{GetItemRoute()}/productIntegrationCode/{((OptionGroup)menuItem).Product.IntegrationCode}";
        }

        public override string GetItemByNameRoute(object menuItem)
        {
            return $"{GetItemsRoute(menuItem)}/name/{((OptionGroup)menuItem).Name}";
        }

        public override string GetItemByIntegrationCodeRoute(object menuItem)
        {
            return $"{GetItemsRoute(menuItem)}/integrationCode/{((OptionGroup)menuItem).IntegrationCode}";
        }

        public override string GetItemSchedulesRoute(Schedule scheduleItem)
        {
            throw new NotSupportedException("This operation is not allowed");
        }

        public override ExpandoObject GetItemSchedulePayload(Schedule scheduleItem, long restaurantId)
        {
            throw new NotSupportedException("This operation is not allowed");
        }
    }
}
