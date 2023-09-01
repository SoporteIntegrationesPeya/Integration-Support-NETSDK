///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using System;

namespace ReceptionSdk.Helpers
{
    /// <summary>
    /// Utility class to check parameters
    /// </summary>
    class Ensure
    {
        /// <summary>
        /// Checks an argument to ensure it isn't null.
        /// </summary>
        /// <param name="value">the argument value to check</param>
        /// <param name="name">the name of the argument</param>
        public static void ArgumentNotNull(object value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException($"Argument {name} cannot be null");
            }
        }

        /// <summary>
        /// Checks a string argument to ensure it isn't null or empty.
        /// </summary>
        /// <param name="value">The argument value to check</param>
        /// <param name="name">The name of the argument</param>
        public static void ArgumentNotNullOrEmptyString(string value, string name)
        {
            ArgumentNotNull(value, name);
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"Argument {name} cannot be empty", name);
            }
        }

        /// <summary>
        /// Checks a long argument to ensure it is a positive value.
        /// </summary>
        /// <param name="value">The argument value to check</param>
        /// <param name="name">The name of the argument</param>
        public static void GreaterThanZero(long value, string name)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Value must be greater than zero", name);
            }
        }

        /// <summary>
        /// Checks a long argument to ensure it is a positive value.
        /// </summary>
        /// <param name="value">The argument value to check</param>
        /// <param name="name">The name of the argument</param>
        public static void GreaterOrEqualsThanZero(long value, string name)
        {
            if (value < 0)
            {
                throw new ArgumentException("Value must be greater or equals than zero", name);
            }
        }

        /// <summary>
        /// Checks a double argument to ensure it is a positive value.
        /// </summary>
        /// <param name="value">The argument value to check</param>
        /// <param name="name">The name of the argument</param>
        public static void GreaterOrEqualsThanZero(double value, string name)
        {
            if (value < 0)
            {
                throw new ArgumentException("Value must be greater or equals than zero", name);
            }
        }

        /// <summary>
        /// Checks dates arguments to ensure the first one is before than the second one.
        /// </summary>
        /// <param name="firstDate">The first date value to check</param>
        /// <param name="secondDate">The second date value to check</param>
        /// <param name="firstDateName">The name of the first argument</param>
        /// <param name="secondDateName">The name of the second argument</param>
        public static void FirstDateBeforeSecond(DateTime firstDate, DateTime secondDate, string firstDateName, string secondDateName)
        {
            if (!(firstDate < secondDate))
            {
                throw new ArgumentException($"Argument {firstDateName} must be before than {secondDateName}", firstDateName);
            }
        }
    }
}
