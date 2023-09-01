using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReceptionSdk.Models
{
    class DiscountCode
    {
        /// <summary>
        /// Discount weekly
        /// </summary>
        public static readonly string WEEKLY = "WEEKLY";

        /// <summary>
        /// Discount for stamps
        /// </summary>
        public static readonly string STAMPS = "STAMPS";

        /// <summary>
        /// Discount for promotions
        /// </summary>
        public static readonly string PROMOTION = "PROMOTION";

        /// <summary>
        /// Discount for vouchers
        /// </summary>
        public static readonly string VOUCHER = "VOUCHER";
    }
}
