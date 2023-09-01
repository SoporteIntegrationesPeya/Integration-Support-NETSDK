///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using System;

namespace ReceptionSdk.Helpers
{
    /// <summary>
    /// Utility class to handle dates
    /// </summary>
    public class DateUtil
    {

        public static readonly string DATE_FORMAT = "yyyy-MM-dd'T'HH:mm:ss'Z'";

        /// <summary>
        /// Returns a date in a string format(yyyy-MM-dd'T'HH:mm:ss'Z')
        /// </summary>
        /// <param name="date">the date to be formatted</param>
        /// <returns>a string representation from a date</returns>
        public static string FormatDate(DateTime date)
        {
            return date.ToString(DATE_FORMAT);
        }

    }
}
