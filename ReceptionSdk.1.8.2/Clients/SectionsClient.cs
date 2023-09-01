///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using ReceptionSdk.Http;
using System;
using System.Dynamic;
using ReceptionSdk.Models;

namespace ReceptionSdk.Clients
{
    /// <summary>
    /// A client for Sections API.
    /// </summary>
    public class SectionsClient : MenusClient
    {
        private ApiConnection Connection { get; set; }

        /// <summary>
        /// Instantiate a new Section API client.
        /// </summary>
        /// <param name="connection">an API connection</param>
        /// <exception cref="ArgumentException">if some error has occurred with params</exception>
        public SectionsClient(ApiConnection connection) : base(connection) { }

        public override ExpandoObject GetItemPayload(object menuItem, long restaurantId)
        {
            return SectionPayload((Section)menuItem, restaurantId);
        }
        private ExpandoObject SectionPayload(Section section, long restaurantId)
        {
            dynamic body = new ExpandoObject();
            body.partnerId = restaurantId;

            if (section.Name != null)
            {
                body.name = section.Name;
            }
            if (section.Index != -1)
            {
                body.index = section.Index;
            }
            if (section.IntegrationCode != null)
            {
                body.integrationCode = section.IntegrationCode;
            }
            if (section.IntegrationName != null)
            {
                body.integrationName = section.IntegrationName;
            }
            if (section.Enabled != null)
            {
                body.enabled = section.Enabled;
            }
            return body;
        }

        public override ExpandoObject GetItemSchedulePayload(Schedule scheduleItem, long restaurantId)
        {
            return SectionSchedulePayload(scheduleItem, restaurantId);
        }

        private ExpandoObject SectionSchedulePayload(Schedule sectionSchedule, long restaurantId)
        {
            dynamic body = new ExpandoObject();
            body.section = new ExpandoObject();
            body.partnerId = restaurantId;

            if (sectionSchedule.Entity != null)
            {
                if (sectionSchedule.Entity.IntegrationCode != null)
                {
                    body.section.integrationCode = sectionSchedule.Entity.IntegrationCode;
                }
                if (sectionSchedule.Entity.Name != null)
                {
                    body.section.name = sectionSchedule.Entity.Name;
                }
            }
            if (sectionSchedule.From != null)
            {
                body.from = sectionSchedule.From;
            }
            if (sectionSchedule.To != null)
            {
                body.to = sectionSchedule.To;
            }
            if (sectionSchedule.Day != null)
            {
                body.day = sectionSchedule.Day;
            }
            return body;
        }

        public override string GetItemRoute()
        {
            return "section";
        }

        public override string GetItemsRoute(object menuItem)
        {
            return GetItemRoute();
        }

        public override string GetItemByNameRoute(object menuItem)
        {
            return $"{GetItemsRoute(menuItem)}/name/{((Section)menuItem).Name}";
        }

        public override string GetItemByIntegrationCodeRoute(object menuItem)
        {
            return $"{GetItemsRoute(menuItem)}/integrationCode/{((Section)menuItem).IntegrationCode}";
        }

        public override string GetItemSchedulesRoute(Schedule scheduleItem)
        {
            return $"{GetItemsRoute(scheduleItem)}/name/{((Schedule)scheduleItem).Entity.Name}/schedule";
        }
    }
}
