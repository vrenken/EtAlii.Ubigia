// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Portable;

using EtAlii.xTechnology.MicroContainer;
using PCLStorage;


public class PortableStorageExtension : IExtension
{
    private readonly IFolder _localStorage;

    public PortableStorageExtension(IFolder localStorage)
    {
        _localStorage = localStorage;
    }

    public void Initialize(IRegisterOnlyContainer container)
    {
        var scaffoldings = new IScaffolding[]
        {
            new PortableFactoryScaffolding(_localStorage),
        };

        foreach (var scaffolding in scaffoldings)
        {
            scaffolding.Register(container);
        }
    }
}
