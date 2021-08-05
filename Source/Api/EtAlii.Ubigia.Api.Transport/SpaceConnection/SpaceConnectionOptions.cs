// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using Microsoft.Extensions.Configuration;

    public class SpaceConnectionOptions : ConfigurationBase, ISpaceConnectionOptions
    {
        /// <inheritdoc />
        public IConfiguration ConfigurationRoot { get; }

        /// <inheritdoc />
        public ISpaceTransport Transport { get; private set; }

        /// <inheritdoc />
        public string Space { get; private set; }

        public SpaceConnectionOptions(IConfiguration configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
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
