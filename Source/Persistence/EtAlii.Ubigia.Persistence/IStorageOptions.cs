// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

using EtAlii.xTechnology.MicroContainer;
using Microsoft.Extensions.Configuration;

public interface IStorageOptions
{
    IConfigurationRoot ConfigurationRoot { get; }
    IExtension[] Extensions { get; }
    string Name { get; }

    IStorageOptions Use(string name);
    IStorageOptions Use(IExtension[] extensions);
    IStorageOptions Use<TStorage>()
        where TStorage : class, IStorage;

    IStorage GetStorage(Container container);

}
