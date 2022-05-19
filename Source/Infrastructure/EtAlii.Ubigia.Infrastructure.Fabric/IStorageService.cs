// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.Ubigia.Persistence;
    using EtAlii.xTechnology.Hosting;

    public interface IStorageService : IBackgroundService
    {
        IStorage Storage { get; }
    }
}
