// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
	public interface ISpaceConnectionConfiguration : IExtensible
    {
        ISpaceTransport Transport { get; }

        string Space { get; }

        ISpaceConnectionConfiguration Use(ISpaceTransport transport);
        ISpaceConnectionConfiguration Use(string space);
    }
}
