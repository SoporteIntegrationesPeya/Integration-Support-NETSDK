namespace ReceptionSdk.Enums
{
    /// <summary>
    /// All available operations types
    /// </summary>
    public enum OrderProductModificationType
    {
        /// <summary>
        /// Addition a product to reconcile
        /// </summary>
        ADDITION,
        
        /// <summary>
        /// Change a product to reconcile
        /// </summary>
        CHANGE,
        
        /// <summary>
        /// Replacement a product to reconcile
        /// </summary>
        REPLACEMENT,
        
        /// <summary>
        /// Removal a product to reconcile
        /// </summary>
        REMOVAL
    }
}