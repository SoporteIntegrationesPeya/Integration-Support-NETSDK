///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using ReceptionSdk.Http;
using ReceptionSdk.Helpers;
using ReceptionSdk.Exceptions;
using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Net;
using ReceptionSdk.Models;
using RestSharp;

namespace ReceptionSdk.Clients
{
    /// <summary>
    /// A client for Products API.
    /// </summary>
    public class ProductsClient : MenusClient
    {
        private ApiConnection Connection { get; set; }

        /// <summary>
        /// Instantiate a new Products API client.
        /// </summary>
        /// <param name="connection">an API connection</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        public ProductsClient(ApiConnection connection) : base(connection) { }

        public override ExpandoObject GetItemPayload(object menuItem, long restaurantId)
        {
            return ProductPayload((Product)menuItem, restaurantId);
        }

        private ExpandoObject ProductPayload(Product product, long restaurantId)
        {

            dynamic body = new ExpandoObject();
            body.restaurant = new ExpandoObject();
            body.section = new ExpandoObject();
            body.restaurant.id = restaurantId;
            if (product.Section != null)
            {
                if (product.Section.IntegrationCode != null)
                {
                    body.section.integrationCode = product.Section.IntegrationCode;
                }
                if (product.Section.Name != null)
                {
                    body.section.name = product.Section.Name;
                }
            }
            if (product.IntegrationCode != null)
            {
                body.integrationCode = product.IntegrationCode;
            }
            if (product.Gtin != null)
            {
                body.gtin = product.Gtin;
            }
            if (product.IntegrationName != null)
            {
                body.integrationName = product.IntegrationName;
            }
            if (product.Name != null)
            {
                body.name = product.Name;
            }
            if (product.Price != null)
            {
                body.price = product.Price;
            }
            if (product.Image != null)
            {
                body.image = product.Image;
            }
            if (product.Description != null)
            {
                body.description = product.Description;
            }
            if (product.MeasurementUnit != null)
            {
                body.measurementUnit = product.MeasurementUnit;
            }
            if (product.PrescriptionBehaviour != null)
            {
                body.prescriptionBehaviour = product.PrescriptionBehaviour;
            }
            if (product.RequiresAgeCheck != null)
            {
                body.requiresAgeCheck = product.RequiresAgeCheck;
            }
            if (product.ContentQuantity !=  null)
            {
                body.contentQuantity = product.ContentQuantity;
            }
            if (product.Index != null)
            {
                body.index = product.Index;
            }
            if (product.Enabled !=  null)
            {
                body.enabled = product.Enabled;
            }
            return body;
        }

        public override string GetItemRoute()
        {
            return "product";
        }

        public override string GetItemsRoute(object menuItem)
        {
            return $"{GetItemRoute()}/sectionIntegrationCode/{((Product)menuItem).Section.IntegrationCode}";
        }

        public override string GetItemByNameRoute(object menuItem)
        {
            return $"{GetItemsRoute(menuItem)}/name/{((Product)menuItem).Name}";
        }

        public override string GetItemByIntegrationCodeRoute(object menuItem)
        {
            return $"{GetItemsRoute(menuItem)}/integrationCode/{((Product)menuItem).IntegrationCode}"; ;
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
