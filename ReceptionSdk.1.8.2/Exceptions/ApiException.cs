///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using System;
using System.Collections.Generic;
using ReceptionSdk.Helpers;
using ReceptionSdk.Http;

namespace ReceptionSdk.Exceptions
{
    /// <summary>
    /// The ApiException represents an API error
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// The returned error code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Constructs a new exception
        /// </summary>
        public ApiException() : base()
        {
        }

        /// <summary>
        /// Constructs a new exception with the specified detail message
        /// </summary>
        /// <param name="message">the detail message</param>
        public ApiException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructs a new exception with the specified detail message and the cause exception
        /// </summary>
        /// <param name="message">the detail message</param>
        /// <param name="inner">the cause exception</param>
        public ApiException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Constructs a new exception with the specified Api error code and the cause exception
        /// </summary>
        /// <param name="inner">the cause exception</param>
        /// <param name="code">the Api error code</param>
        public ApiException(Exception inner, string code) : base(inner.Message, inner)
        {
            this.Code = code;
        }

        /// <summary>
        /// Constructs a new exception based on the rest client response
        /// </summary>
        /// <param name="response">the rest client response</param>
        public ApiException(Response response) : base(BuildExceptionFromJson(response)?.BuildMessage())
        {
            this.Code = BuildExceptionFromJson(response)?.Code;
        }

        /// <summary>
        /// Constructs a new exception with the specified detail message and error code
        /// </summary>
        /// <param name="message">the detail message</param>
        /// <param name="code">the Api error code</param>
        public ApiException(string message, string code) : base(message)
        {
            this.Code = code;
        }

        private static ErrorMessage BuildExceptionFromJson(Response response)
        {
            ErrorMessage message;
            try
            {
                ApiDeserializer deserializer = new ApiDeserializer();
                message = deserializer.Deserialize<ErrorMessage>(response.Content);
            }
            catch (Exception)
            {
                message = new ErrorMessage();
                message.Code = response.StatusCode.ToString();
                message.Messages = new List<string> { response.Content };
            }
            return message;
        }

        private class ErrorMessage
        {
            public string Code { get; set; }
            public List<string> Messages { get; set; }

            public string BuildMessage()
            {
                string message = "";
                for (int i = 0; i < this.Messages.Count; i++)
                {
                    string error = this.Messages[i];
                    message += error;
                    if (i < this.Messages.Count - 1)
                    {
                        message += ", ";
                    }
                }
                return message;
            }
        }
    }
}
