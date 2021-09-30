// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Ntfs
{
    using EtAlii.xTechnology.Hosting;

    public class StorageServiceFactory : IServiceFactory
    {
        public IService Create(ServiceConfiguration serviceConfiguration) => new StorageService(serviceConfiguration);
    }
}
