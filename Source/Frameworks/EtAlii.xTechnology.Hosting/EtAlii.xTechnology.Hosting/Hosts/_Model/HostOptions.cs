// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class HostOptions : IExtensible
    {
        /// <summary>
        /// The configuration root instance for the current host application.
        /// </summary>
        public IConfigurationRoot ConfigurationRoot { get; }

        public ICommand[] Commands { get; private set; }

        /// <summary>
        /// True when the host will be wrapped in a decorator that allows starting, stopping and restarting of the host.
        /// </summary>
        public bool AddHostWrapper { get; private set; }

        /// <summary>
        /// The factory function responsible for creating the actual host.
        /// </summary>
        public Func<IHost> CreateHost { get; private set; }

        public IHostServicesFactory ServiceFactory { get; private set; }

        /// <inheritdoc />
        IExtension[] IExtensible.Extensions { get; set; }

        public HostOptions(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
            ((IExtensible)this).Extensions = new IExtension[] { new CommonHostExtension(this) };
            Commands = Array.Empty<ICommand>();
        }

        public HostOptions Use(params ICommand[] commands)
        {
            Commands = Commands
                .Concat(commands)
                .Distinct()
                .ToArray();

            return this;
        }

        /// <summary>
        /// Instruct the host configuration to use the provided host factory method.
        /// </summary>
        /// <param name="createHost"></param>
        /// <returns></returns>
        public HostOptions Use(Func<IHost> createHost)
        {
            if (CreateHost != null)
            {
                throw new InvalidOperationException("Host factory configuration already done");
            }

            CreateHost = createHost;

            return this;
        }

        public HostOptions Use<THostServicesFactory>()
            where THostServicesFactory : IHostServicesFactory, new()
        {
            if (ServiceFactory != null)
            {
                throw new InvalidOperationException("Service factory configuration already done");
            }

            ServiceFactory = new THostServicesFactory();
            return this;
        }

        /// <summary>
        /// Instruct the host configuration that the host should be wrapped.
        /// This is mostly useful during runtime, as the wrapper allows the application to start, stop and restart the host.
        /// </summary>
        /// <param name="useWrapper"></param>
        /// <returns></returns>
        public HostOptions UseWrapper(bool useWrapper)
        {
            AddHostWrapper = useWrapper;

            return this;
        }
    }
}
