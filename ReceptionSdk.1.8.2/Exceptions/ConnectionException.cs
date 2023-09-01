///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using System;

namespace ReceptionSdk.Exceptions
{
    /// <summary>
    /// Connection exception
    /// </summary>
    class ConnectionException : Exception
    {
        /// <summary>
        /// Instantiate a new ConnectionException
        /// </summary>
        /// <param name="message">description of the exception</param>
        /// <param name="inner">exception cause</param>
        public ConnectionException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Instantiate a new ConnectionException
        /// </summary>
        /// <param name="message">description of the exception</param>
        public ConnectionException(string message) : base(message)
        {
        }
    }
}
