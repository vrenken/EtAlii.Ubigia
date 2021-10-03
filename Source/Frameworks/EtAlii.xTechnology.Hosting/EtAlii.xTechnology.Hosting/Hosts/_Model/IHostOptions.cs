// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using Microsoft.Extensions.Configuration;

    public interface IHostOptions
    {
        /// <summary>
        /// The configuration root instance for the current host application.
        /// </summary>
        IConfigurationRoot ConfigurationRoot { get; }

        /// <summary>
        /// Returns the array of all extensions that got registered for this host.
        /// </summary>
        IHostExtension[] Extensions { get; }

        ICommand[] Commands { get; }

        /// <summary>
        /// The factory function responsible for creating the actual host.
        /// </summary>
        Func<IHost> CreateHost { get; }

        /// <summary>
        /// True when the host will be wrapped in a decorator that allows starting, stopping and restarting of the host.
        /// </summary>
        bool UseWrapper { get; }

        /// <summary>
        /// The details (variables) that got found in the configuration.
        /// This is very useful for testing as it allows for changing the ports/ip addresses on the fly.
        /// </summary>
        ConfigurationDetails Details { get; }

        IHostServicesFactory ServiceFactory { get; }

        /// <summary>
        /// Instructs the host configuration to use the extensions provided.
        /// </summary>
        /// <param name="extensions"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        IHostOptions Use(params IHostExtension[] extensions);

        IHostOptions Use(params ICommand[] commands);

        IHostOptions Use<THostServicesFactory>()
            where THostServicesFactory : IHostServicesFactory, new();

        /// <summary>
        /// Instruct the host configuration to use the provided ConfigurationDetails and ConfigurationRoot.
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        IHostOptions Use(ConfigurationDetails details);

        /// <summary>
        /// Instruct the host configuration to use the provided host factory method.
        /// </summary>
        /// <param name="createHost"></param>
        /// <returns></returns>
        IHostOptions Use(Func<IHost> createHost);

        /// <summary>
        /// Instruct the host configuration that the host should be wrapped.
        /// This is mostly useful during runtime, as the wrapper allows the application to start, stop and restart the host.
        /// </summary>
        /// <param name="useWrapper"></param>
        /// <returns></returns>
        IHostOptions Use(bool useWrapper);
    }
}
