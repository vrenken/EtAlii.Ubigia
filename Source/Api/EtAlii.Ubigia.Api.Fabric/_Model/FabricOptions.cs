// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using System;
    using System.Threading;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public sealed class FabricOptions : IExtensible
    {
        /// <summary>
        /// The client configuration root that will be used to configure the fabric context.
        /// </summary>
        public IConfigurationRoot ConfigurationRoot { get; }

        /// <summary>
        /// The Connection that should be used to communicate with the backend.
        /// </summary>
        public IDataConnection Connection => _connection?.Value;
        private Lazy<IDataConnection> _connection;

        /// <summary>
        /// Set this property to true to enable client-side caching. It makes sure that the immutable entries
        /// and relations are kept on the client.This reduces network traffic but requires more local memory.
        /// </summary>
        public bool CachingEnabled {get; private set; }

        /// <inheritdoc/>
        IExtension[] IExtensible.Extensions { get => _extensions; set => _extensions = value; }
        private IExtension[] _extensions;

        public FabricOptions(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;

            //CachingEnabled = false;
            CachingEnabled = true; // TODO: Caching does not work yet.

            _extensions = new IExtension[] { new CommonFabricExtension(this) };
        }

        /// <summary>
        /// Set the connection that should be used when instantiating a FabricContext.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        /// <exception cref="InvalidInfrastructureOperationException"></exception>
        public FabricOptions Use(IDataConnection connection)
        {
            if (!connection.IsConnected && connection is not DataConnectionStub)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            _connection = new Lazy<IDataConnection>(connection);
            return this;
        }

        /// <summary>
        /// Set the connection options that should be used when instantiating a FabricContext.
        /// </summary>
        /// <param name="dataConnectionOptions"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public FabricOptions Use(DataConnectionOptions dataConnectionOptions)
        {
            if (dataConnectionOptions is null)
            {
                throw new ArgumentNullException(nameof(dataConnectionOptions));
            }
            _connection = new Lazy<IDataConnection>(() =>
            {
                var dataConnection = Factory.Create<IDataConnection>(dataConnectionOptions);
                if (!dataConnection.IsConnected)
                {
                    var task = dataConnection.Open();
                    task.Wait();
                }

                return dataConnection;
            }, LazyThreadSafetyMode.ExecutionAndPublication);
            return this;
        }

        /// <summary>
        /// When cachingEnabled is set to true the instantiated FabricContext is configured to use traversal caching.
        /// </summary>
        /// <param name="cachingEnabled"></param>
        /// <returns></returns>
        public FabricOptions UseCaching(bool cachingEnabled)
        {
            //CachingEnabled = false;
            CachingEnabled = true; // TODO: Caching does not work yet.
            return this;
        }
    }
}
