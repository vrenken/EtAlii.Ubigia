// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.xTechnology.MicroContainer;

    public interface IStorageConfiguration
    {
        IStorageExtension[] Extensions { get; }
        string Name { get; }

        IStorageConfiguration Use(string name);
        IStorageConfiguration Use(IStorageExtension[] extensions);
        IStorageConfiguration Use<TStorage>()
            where TStorage : class, IStorage;

        IStorage GetStorage(Container container);

    }
}
