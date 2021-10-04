// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    public interface IHostServicesFactory
    {
        public IService[] Create(IHostOptions options, IHost host);
    }
}
