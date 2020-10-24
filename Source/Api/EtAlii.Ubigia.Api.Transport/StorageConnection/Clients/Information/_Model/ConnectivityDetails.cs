namespace EtAlii.Ubigia.Api.Transport
{
    /// <summary>
    /// A model class that contains connectivity related details. 
    /// </summary>
    public class ConnectivityDetails
    {
        /// <summary>
        /// The transport specific management API address to which the client is connected.  
        /// </summary>
        public string ManagementAddress { get; set; }

        /// <summary>
        /// The transport specific data API address to which the client is connected.  
        /// </summary>
        public string DataAddress { get; set; }
    }
}