// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using Microsoft.Extensions.Configuration;

    public interface ISpaceConnectionOptions : IExtensible
    {
        /// <summary>
        /// The client configuration root used to instantiate the space connection.
        /// </summary>
        IConfigurationRoot ConfigurationRoot { get; }

        ISpaceTransport Transport { get; }

        string Space { get; }

        ISpaceConnectionOptions Use(ISpaceTransport transport);
        ISpaceConnectionOptions Use(string space);
    }
}
