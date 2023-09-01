///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
namespace ReceptionSdk.Exceptions
{
    /// <summary>
    /// Possible API error codes
    /// </summary>
    public class ErrorCode
    {
        ///
        ///You should not see this code but be prepared
        ///
        public const string INTERNAL_SERVER_ERROR = "INTERNAL_SERVER_ERROR";

        ///
        /// When trying to update an order that has been cancelled already
        ///
        public const string ORDER_CANCELLED = "ORDER_CANCELLED";

        ///
        /// When trying to update an order that has been confirmed already
        ///
        public const string ORDER_CONFIRMED = "ORDER_CONFIRMED";

        ///
        /// When trying to update an order that has been rejected already
        ///
        public const string ORDER_REJECTED = "ORDER_REJECTED";
        
        ///
        /// When the token is invalid or you don't have permission
        ///
        public const string INVALID_TOKEN = "INVALID_TOKEN";

        ///
        /// When the parameters are invalid
        ///
        public const string INVALID_PARAMS = "INVALID_PARAMS";

        ///
        /// When the resource not exists
        ///
        public const string NOT_EXISTS = "NOT_EXISTS";

        ///
        /// When some parameter is missing in the request
        ///
        public const string MISSING_PARAM = "MISSING_PARAM";

        ///
        /// When a product with the same integration code already exists
        ///
        public const string MENU_ITEM_ALREADY_EXISTS = "MENU_ITEM_ALREADY_EXISTS";

        ///
        /// When the partner does not already have a menu
        ///
        public const string PARTNER_HAS_NO_MENU = "PARTNER_HAS_NO_MENU";

        ///
        /// When the section associated with the product could not be created
        ///
        public const string SECTION_CREATION_ERROR = "SECTION_CREATION_ERROR";

        ///
        /// When the image associated with the product could not be downloaded
        ///
        public const string DOWNLOADING_PRODUCT_IMAGE = "DOWNLOADING_PRODUCT_IMAGE";

        ///
        /// When the image associated with the product could not be uploaded
        ///
        public const string UPLOADING_PRODUCT_IMAGE = "UPLOADING_PRODUCT_IMAGE";

        ///
        /// When trying to create a new product with a new vertical partner
        ///
        public const string NOT_ALLOWED = "NOT_ALLOWED";

        ///
        /// When the product is being validated by Pedidos Ya
        ///
        public const string PRODUCT_VALIDATE_PROCESSING = "PRODUCT_VALIDATE_PROCESSING";

        ///
        /// When the product cannot be sold in Pedidos Ya
        ///
        public const string PRODUCT_NOT_AVAILABLE_FOR_SALE = "PRODUCT_NOT_AVAILABLE_FOR_SALE";

        ///
        /// When a request to validate the product was created by Pedidos Ya
        ///
        public const string PRODUCT_CREATION_REQUEST_CREATED = "PRODUCT_CREATION_REQUEST_CREATED";

        ///
        /// When the partner does not use catalog
        ///
        public const string PARTNER_NOT_USE_CATALOGUE = "PARTNER_NOT_USE_CATALOGUE";
    }
}
