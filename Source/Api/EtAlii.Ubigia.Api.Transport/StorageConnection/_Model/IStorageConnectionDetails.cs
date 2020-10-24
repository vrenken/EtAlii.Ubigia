namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public interface IStorageConnectionDetails
    {
        /// <summary>
        /// The address where the management API is located. 
        /// </summary>
        Uri ManagementAddress { get; }
        
        /// <summary>
        /// The address where the data API is located. 
        /// </summary>
        Uri DataAddress { get; }
    }
}