///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The Order class holds all the order data. The order details, the client information, the payment method, etc.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// The order identification number
        /// </summary>
        [DeserializeAs(Name = "id")]
        public long Id { get; set; }

        /// <summary>
        /// The order identification code
        /// </summary>
        [DeserializeAs(Name = "code")]
        public string Code { get; set; }

        /// <summary>
        /// The order state
        /// </summary>
        [DeserializeAs(Name = "state")]
        public string State { get; set; }

        /// <summary>
        /// The user address
        /// </summary>
        [DeserializeAs(Name = "address")]
        public Address Address { get; set; }

        /// <summary>
        /// Return the application type that the order has been created.
        /// It's one of the following: WEB, CALL, IPHONE, ANDROID, WIDGET, WINDOWS_PHONE, WEB_MOBILE, IPAD
        /// </summary>
        [DeserializeAs(Name = "application")]
        public string Application { get; set; }

        /// <summary>
        /// The order details data
        /// </summary>
        [DeserializeAs(Name = "details")]
        public List<Detail> Details { get; set; }

        /// <summary>
        /// The order notes
        /// </summary>
        [DeserializeAs(Name = "notes")]
        public string Notes { get; set; }

        /// <summary>
        /// The payment method of the order
        /// </summary>
        [DeserializeAs(Name = "payment")]
        public Payment Payment { get; set; }

        /// <summary>
        /// <c>true</c> if the order is for pickup, <c>false</c> if is for delivery
        /// </summary>
        [DeserializeAs(Name = "pickup")]
        public bool Pickup { get; set; } = false;

        /// <summary>
        /// <c>true</c> if the order is an express order, <c>false</c> if is for delivery
        /// </summary>
        [DeserializeAs(Name = "express")]
        public bool Express { get; set; } = false;

        /// <summary>
        /// The order timestamp
        /// </summary>
        [DeserializeAs(Name = "timestamp")]
        public long Timestamp { get; set; }

        /// <summary>
        /// The order registration date
        /// </summary>
        [DeserializeAs(Name = "registeredDate")]
        public DateTime RegisteredDate { get; set; }

        /// <summary>
        /// The order pickup date, <c>null</c> if the order it's delivered by the restaurant
        /// </summary>
        [DeserializeAs(Name = "pickupDate")]
        public DateTime PickupDate { get; set; }

        /// <summary>
        /// Returns the delivery date of the order
        /// </summary>
        [DeserializeAs(Name = "deliveryDate")]
        public DateTime DeliveryDate { get; set; }

        /// <summary>
        /// The order response date
        /// </summary>
        [DeserializeAs(Name = "responseDate")]
        public DateTime ResponseDate { get; set; }

        /// <summary>
        /// The order dispatch date
        /// </summary>
        [DeserializeAs(Name = "dispatchDate")]
        public DateTime DispatchDate { get; set; }

        /// <summary>
        /// The user who made the order
        /// </summary>
        [DeserializeAs(Name = "user")]
        public User User { get; set; }

        /// <summary>
        /// The order restaurant
        /// </summary>
        [DeserializeAs(Name = "restaurant")]
        public Restaurant Restaurant { get; set; }

        /// <summary>
        /// The portal of the order that belongs to
        /// </summary>
        [DeserializeAs(Name = "portal")]
        public Portal Portal { get; set; }
        
        /// <summary>
        /// The white label of the order that belongs to
        /// </summary>
        [DeserializeAs(Name = "whiteLabel")]
        public string WhiteLabel { get; set; }

        /// <summary>
        /// <c>true</c> if the order it's an pre-order, <c>false</c> if is for immediately delivery
        /// </summary>
        [DeserializeAs(Name = "preOrder")]
        public bool PreOrder { get; set; } = false;

        /// <summary>
        /// <c>true</c> if the order it's an order with logistics, <c>false</c> otherwise
        /// </summary>
        [DeserializeAs(Name = "logistics")]
        public bool Logistics { get; set; } = false;

        /// <summary>
        /// The orders discounts
        /// </summary>
        [DeserializeAs(Name = "discounts")]
        public List<Discount> Discounts { get; set; }

        /// <summary>
        /// The orders attachments
        /// </summary>
        [DeserializeAs(Name = "attachments")]
        public List<Attachment> Attachments { get; set; }

        /// <summary>
        /// Default Order constructor
        /// </summary>
        public Order() : base()
        {
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }
    }
}