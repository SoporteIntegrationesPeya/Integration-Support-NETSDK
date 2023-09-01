using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReceptionSdk.Models
{
    public class TrackingState
    {
        /// <summary>
        /// State of the tracking when it failed
        /// </summary>
        public const string FAILURE = "FAILURE";

        /// <summary>
        /// State of the tracking when a driver is being requested
        /// </summary>
        public const string REQUESTING_DRIVER = "REQUESTING_DRIVER";

        /// <summary>
        /// State of the tracking when the order is being transmitted
        /// </summary>
        public const string TRANSMITTING = "TRANSMITTING";

        /// <summary>
        /// State of the tracking when the order is finally transmitted
        /// </summary>
        public const string TRANSMITTED = "TRANSMITTED";

        /// <summary>
        /// State of the tracking when the order is being prepared
        /// </summary>
        public const string PREPARING = "PREPARING";

        /// <summary>
        /// State of the tracking when the order is being delivered
        /// </summary>
        public const string DELIVERING = "DELIVERING";

        /// <summary>
        /// State of the tracking when the order is finally delivered
        /// </summary>
        public const string DELIVERED = "DELIVERED";

        /// <summary>
        /// State of the tracking when the order is closed and no live tracking happens
        /// </summary>
        public const string CLOSED = "CLOSED";
    }
}
