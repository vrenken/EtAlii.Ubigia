namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public interface IStorageConnection : IConnection, IDisposable
    {
        /// <summary>
        /// The transport with which the connection is made.
        /// </summary>
        IStorageTransport Transport { get; }
        
        /// <summary>
        /// A context through which to call storage related RPC's. 
        /// </summary>
        IStorageContext Storages { get; }
        /// <summary>
        /// A context through which to call account related RPC's. 
        /// </summary>
        IAccountContext Accounts { get; }
        /// <summary>
        /// A context through which to call space related RPC's. 
        /// </summary>
        ISpaceContext Spaces { get; }
        
        /// <summary>
        /// Additional details regarding the StorageConnection.
        /// </summary>
        IStorageConnectionDetails Details { get; }
        /// <summary>
        /// The Configuration used to instantiate this StorageConnection.
        /// </summary>
        IStorageConnectionConfiguration Configuration { get; }
    }

    public interface IStorageConnection<out TTransport> : IStorageConnection
        where TTransport: IStorageTransport
    {
        /// <summary>
        /// A typed version of the transport that the storage connection is using.
        /// </summary>
        new TTransport Transport { get; }
    }
}
