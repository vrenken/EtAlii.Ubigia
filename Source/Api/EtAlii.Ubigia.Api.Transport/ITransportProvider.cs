// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public interface ITransportProvider
    {
        ISpaceTransport GetSpaceTransport(Uri address);
    }
}