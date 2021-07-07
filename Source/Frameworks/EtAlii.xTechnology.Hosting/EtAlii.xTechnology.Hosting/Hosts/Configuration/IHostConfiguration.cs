// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using Microsoft.Extensions.Configuration;

    public interface IHostConfiguration
    {
        IHostExtension[] Extensions { get; }
        ICommand[] Commands { get; }

        Func<IHost> CreateHost { get; }

        bool UseWrapper { get; }
        Func<IHost, SystemFactory, ServiceFactory, ModuleFactory, ISystem[]> CreateSystems { get; }

        IHostConfiguration Use(params IHostExtension[] extensions);
        IHostConfiguration Use(params IConfigurationSection[] systemConfigurations);
        IHostConfiguration Use(Func<IHost, SystemFactory, ServiceFactory, ModuleFactory, ISystem[]> createSystems);

        IHostConfiguration Use(params ICommand[] commands);

        /// <summary>
        /// Instruct the host configuration to use the provided ConfigurationDetails and ConfigurationRoot.
        /// </summary>
        /// <param name="details"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        IHostConfiguration Use(ConfigurationDetails details, IConfigurationRoot root);

        IHostConfiguration Use(Func<IHost> createHost);
        IHostConfiguration Use(bool useWrapper);
    }
}
