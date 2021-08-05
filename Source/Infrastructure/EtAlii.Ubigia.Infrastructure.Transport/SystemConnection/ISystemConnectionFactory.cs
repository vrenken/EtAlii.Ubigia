// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.Ubigia.Infrastructure.Functional;

    public interface ISystemConnectionFactory
    {
        ISystemConnection Create(ISystemConnectionOptions options);
    }
}