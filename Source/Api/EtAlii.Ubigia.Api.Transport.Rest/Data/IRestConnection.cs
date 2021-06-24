// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest
{
    public interface IRestConnection : IConnection
    {
        IRestInfrastructureClient Client { get; }
        IAddressFactory AddressFactory { get; }
    }
}
