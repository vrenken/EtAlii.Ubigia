// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.InMemory;

using EtAlii.xTechnology.MicroContainer;

public class InMemoryStorageExtension : IExtension
{
    public void Initialize(IRegisterOnlyContainer container)
    {
        var scaffoldings = new IScaffolding[]
        {
            new InMemoryFactoryScaffolding(),
        };

        foreach (var scaffolding in scaffoldings)
        {
            scaffolding.Register(container);
        }
    }
}
