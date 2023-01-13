// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using EtAlii.xTechnology.MicroContainer;

internal class SystemScaffolding : IScaffolding
{
    public void Register(IRegisterOnlyContainer container)
    {
        container.Register<ISystemConnectionCreationProxy, SystemConnectionCreationProxy>();
        container.Register<ISystemStatusContext, SystemStatusContext>();
        container.Register<ISystemStatusChecker, SystemStatusChecker>();
    }
}
