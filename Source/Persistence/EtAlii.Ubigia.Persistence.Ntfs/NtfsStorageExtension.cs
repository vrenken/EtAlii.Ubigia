﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Ntfs;

using EtAlii.xTechnology.MicroContainer;

public class NtfsStorageExtension : IExtension
{
    private readonly string _baseFolder;

    public NtfsStorageExtension(string baseFolder)
    {
        _baseFolder = baseFolder;
    }

    public void Initialize(IRegisterOnlyContainer container)
    {
        var scaffoldings = new IScaffolding[]
        {
            new NtfsFactoryScaffolding()
        };

        foreach (var scaffolding in scaffoldings)
        {
            scaffolding.Register(container);
        }
        container.RegisterInitializer<IPathBuilder>(pb => ((NtfsPathBuilder)pb).Initialize(_baseFolder));
    }
}
