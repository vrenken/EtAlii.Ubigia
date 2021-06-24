// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;

    public interface ISystemConnectionCreationProxy
    {
        ISystemConnection Request();

        void Initialize(Func<ISystemConnection> create);
    }
}