using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReceptionSdk.Enums
{
    /// <summary>
    /// All available operations types
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// Create
        /// </summary>
        CREATE,

        /// <summary>
        /// Update
        /// </summary>
        UPDATE,

        /// <summary>
        /// Delete
        /// </summary>
        DELETE,

        /// <summary>
        /// Get all
        /// </summary>
        GET_ALL,

        /// <summary>
        /// Get by name
        /// </summary>
        GET_BY_NAME,

        /// <summary>
        /// Get by integration code
        /// </summary>
        GET_BY_INTEGRATION_CODE
    }
}
