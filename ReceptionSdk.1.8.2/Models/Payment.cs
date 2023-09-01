///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp;
using RestSharp.Deserializers;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The Payment class holds all payment options information
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// The payment method id
        /// </summary>
        [DeserializeAs(Name = "id")]
        public long Id { get; set; }

        /// <summary>
        /// The amount of the order without discount
        /// </summary>
        [DeserializeAs(Name = "amountNoDiscount")]
        public double AmountNoDiscount { get; set; }

        /// <summary>
        /// The currency symbol of the payment method
        /// </summary>
        [DeserializeAs(Name = "currencySymbol")]
        public string CurrencySymbol { get; set; }
        
        /// <summary>
        /// The payment method description: 'Cash', 'Credit card', etc.
        /// </summary>
        [DeserializeAs(Name = "method")]
        public string Method { get; set; }

        /// <summary>
        /// The payment notes, for example: '$100 with credit card and $17 with cash'
        /// </summary>
        [DeserializeAs(Name = "notes")]
        public string Notes { get; set; }

        /// <summary>
        /// <c>true</c> if it's online payment
        /// </summary>
        [DeserializeAs(Name = "online")]
        public bool Online { get; set; } = false;

        /// <summary>
        /// The user payment amount
        /// </summary>
        [DeserializeAs(Name = "paymentAmount")]
        public double PaymentAmount { get; set; }

        /// <summary>
        /// The shipping cost of the order
        /// </summary>
        [DeserializeAs(Name = "shipping")]
        public double Shipping { get; set; }

        /// <summary>
        /// The shipping cost of the order without discount
        /// </summary>
        [DeserializeAs(Name = "shippingNoDiscount")]
        public double ShippingNoDiscount { get; set; }

        /// <summary>
        /// The subtotal is the sum of the details totals plus shippingNoDiscount
        /// </summary>
        [DeserializeAs(Name = "subtotal")]
        public double Subtotal { get; set; }

        /// <summary>
        /// The total price of the order
        /// </summary>
        [DeserializeAs(Name = "total")]
        public double Total { get; set; }

        /// <summary>
        /// The total taxes of the order
        /// </summary>
        [DeserializeAs(Name = "tax")]
        public double Tax { get; set; }

        /// <summary>
        /// The card used in the order
        /// </summary>
        [DeserializeAs(Name = "card")]
        public Card Card { get; set; }

        /// <summary>
        /// Default Payment constructor
        /// </summary>
        public Payment() : base()
        {
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }
    }
}