// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public interface ISpaceConnectionOptions : IExtensible
    {
        IConfigurationRoot ConfigurationRoot { get; }
        ISpaceTransport Transport { get; }

        string Space { get; }

        ISpaceConnectionOptions Use(ISpaceTransport transport);
        ISpaceConnectionOptions Use(string space);
    }
}
