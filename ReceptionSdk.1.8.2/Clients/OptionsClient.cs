///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using ReceptionSdk.Http;
using System;
using System.Dynamic;
using System.Net;
using ReceptionSdk.Models;

namespace ReceptionSdk.Clients
{
    /// <summary>
    /// A client for Options API.
    /// </summary>
    public class OptionsClient : MenusClient
    {
        private ApiConnection Connection { get; set; }

        /// <summary>
        /// Instantiate a new Options API client.
        /// </summary>
        /// <param name="connection">an API connection</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        public OptionsClient(ApiConnection connection) : base(connection) { }

        public override ExpandoObject GetItemPayload(object menuItem, long restaurantId)
        {
            return OptionPayload((Option)menuItem, restaurantId);
        }

        private ExpandoObject OptionPayload(Option option, long restaurantId)
        {
            dynamic body = new ExpandoObject();
            body.optionGroup = new ExpandoObject();
            body.optionGroup.product = new ExpandoObject();
            body.optionGroup.product.partnerId = restaurantId;

            if (option.OptionGroup != null)
            {
                if (option.OptionGroup.Product.IntegrationCode != null)
                {
                    body.optionGroup.product.integrationCode = option.OptionGroup.Product.IntegrationCode;
                }
                if (option.OptionGroup.IntegrationCode != null)
                {
                    body.optionGroup.integrationCode = option.OptionGroup.IntegrationCode;
                }
                if (option.OptionGroup.IntegrationName != null)
                {
                    body.optionGroup.integrationName = option.OptionGroup.IntegrationName;
                }
                if (option.OptionGroup.Name != null)
                {
                    body.optionGroup.name = option.OptionGroup.Name;
                }
                if (option.OptionGroup.MaximumQuantity != null)
                {
                    body.optionGroup.maximumQuantity = option.OptionGroup.MaximumQuantity;
                }
                if (option.OptionGroup.MinumumQuantity != null)
                {
                    body.optionGroup.minimumQuantity = option.OptionGroup.MinumumQuantity;
                }
                if (option.Name != null)
                {
                    body.name = option.Name;
                }
                if (option.IntegrationCode != null)
                {
                    body.integrationCode = option.IntegrationCode;
                }
                if (option.IntegrationName != null)
                {
                    body.integrationName = option.IntegrationName;
                }
                if (option.RequiresAgeCheck != null)
                {
                    body.requiresAgeCheck = option.RequiresAgeCheck;
                }
                if (option.Price != null)
                {
                    body.price = option.Price;
                }
                if (option.Enabled != null)
                {
                    body.enabled = option.Enabled;
                }
                if (option.Quantity != null)
                {
                    body.quantity = option.Quantity;
                }
                if (option.Index != null)
                {
                    body.index = option.Index;
                }
                if (option.ModifiesPrice != null)
                {
                    body.modifiesPrice = option.ModifiesPrice;
                }
            }
            return body;
        }

        public override string GetItemRoute()
        {
            return "option";
        }

        public override string GetItemsRoute(object menuItem)
        {
            return $"{GetItemRoute()}/product/{((Option)menuItem).OptionGroup.Product.IntegrationCode}" +
                $"/optionGroup/{((Option)menuItem).OptionGroup.IntegrationCode}";
        }

        public override string GetItemByNameRoute(object menuItem)
        {
            throw new NotSupportedException("This operation is not allowed");
        }

        public override string GetItemByIntegrationCodeRoute(object menuItem)
        {
            throw new NotSupportedException("This operation is not allowed");
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
