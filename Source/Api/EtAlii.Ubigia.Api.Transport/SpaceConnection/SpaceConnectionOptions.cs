// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class SpaceConnectionOptions : IExtensible
    {
        /// <summary>
        /// The client configuration root used to instantiate the space connection.
        /// </summary>
        public IConfigurationRoot ConfigurationRoot { get; }

        /// <inheritdoc/>
        IExtension[] IExtensible.Extensions { get; set; }

        /// <summary>
        /// The space transport that should be used to create the connection.
        /// </summary>
        public ISpaceTransport Transport { get; private set; }

        /// <summary>
        /// The space that should be connected to.
        /// </summary>
        public string Space { get; private set; }

        public SpaceConnectionOptions(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
            ((IExtensible)this).Extensions = new IExtension[]{ new CommonSpaceConnectionExtension(this) };
        }

        /// <summary>
        /// Configure the space transport that should be used to create the connection.
        /// </summary>
        public SpaceConnectionOptions Use(ISpaceTransport transport)
        {
            if (transport == null)
            {
                throw new ArgumentException("No account name specified", nameof(transport));
            }
            if (Transport != null)
            {
                throw new InvalidOperationException("A Transport has already been assigned to this SpaceConnectionOptions");
            }

            Transport = transport;
            return this;
        }

        /// <summary>
        /// Configure the space that should be connected to.
        /// </summary>
        public SpaceConnectionOptions Use(string space)
        {
            if (string.IsNullOrWhiteSpace(space))
            {
                throw new ArgumentException("No space specified", nameof(space));
            }
            if (Space != null)
            {
                throw new InvalidOperationException("A space has already been assigned to this SpaceConnectionOptions");
            }

            Space = space;
            return this;
        }
    }
}
