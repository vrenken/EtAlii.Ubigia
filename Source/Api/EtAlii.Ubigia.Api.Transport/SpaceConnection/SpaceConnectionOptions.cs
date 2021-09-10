// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class SpaceConnectionOptions : ISpaceConnectionOptions
    {
        /// <summary>
        /// The client configuration root used to instantiate the space connection.
        /// </summary>
        public IConfigurationRoot ConfigurationRoot { get; }

        /// <inheritdoc/>
        IExtension[] IExtensible.Extensions { get => _extensions; set => _extensions = value; }
        private IExtension[] _extensions;

        /// <inheritdoc />
        public ISpaceTransport Transport { get; private set; }

        /// <inheritdoc />
        public string Space { get; private set; }

        public SpaceConnectionOptions(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
            _extensions = Array.Empty<IExtension>();
        }

        /// <inheritdoc />
        public ISpaceConnectionOptions Use(ISpaceTransport transport)
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

        /// <inheritdoc />
        public ISpaceConnectionOptions Use(string space)
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
