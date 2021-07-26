// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.Configuration;

    public class HostConfiguration : IHostConfiguration
    {
        public string EnabledImage { get; private set; }
        public string ErrorImage { get; private set; }
        public string DisabledImage { get; private set; }
        public ICommand[] Commands { get; private set; }

        public string HostTitle { get; private set; }
        public string ProductTitle { get; private set; }

        /// <inheritdoc />
        public bool UseWrapper { get; private set; }
        public Func<IHost, SystemFactory, ServiceFactory, ModuleFactory, ISystem[]> CreateSystems { get; private set; }

        /// <inheritdoc />
        public Func<IHost> CreateHost { get; private set; }

        public Action<string> Output { get; private set; }

        /// <inheritdoc />
        public IHostExtension[] Extensions { get; private set; }

        /// <inheritdoc />
        public ConfigurationDetails Details { get; private set; }

        /// <inheritdoc />
        public IConfigurationRoot Root { get; private set; }

        public HostConfiguration()
        {
            Extensions = Array.Empty<IHostExtension>();
            Commands = Array.Empty<ICommand>();
        }

        /// <inheritdoc />
        public IHostConfiguration Use(ConfigurationDetails details, IConfigurationRoot root)
        {
            Details = details;
            Root = root;

            return this;
        }

        public IHostConfiguration Use(string enabledImage, string errorImage, string disabledImage)
        {
            EnabledImage = enabledImage;
            ErrorImage = errorImage;
            DisabledImage = disabledImage;

            return this;
        }

        public IHostConfiguration Use(string productTitle = "EtAlii.xTechnology.Hosting", string hostTitle = "Host")
        {
            ProductTitle = productTitle;
            HostTitle = hostTitle;

            return this;
        }

        public IHostConfiguration Use(params ICommand[] commands)
        {
            Commands = Commands
                .Concat(commands)
                .Distinct()
                .ToArray();

            return this;
        }

        public IHostConfiguration Use(Func<IHost, SystemFactory, ServiceFactory, ModuleFactory, ISystem[]> createSystems)
        {
            if (CreateSystems != null)
            {
                throw new InvalidOperationException("Systems configuration already defined");
            }

            CreateSystems = createSystems;

            return this;
        }

        /// <inheritdoc />
        public IHostConfiguration Use(Func<IHost> createHost)
        {
            if (CreateHost != null)
            {
                throw new InvalidOperationException("Host factory configuration already done");
            }

            CreateHost = createHost;

            return this;
        }

        public IHostConfiguration Use(params IConfigurationSection[] systemConfigurations)
        {
            if (CreateSystems != null)
            {
                throw new InvalidOperationException("Systems configuration already defined");
            }

            CreateSystems = (host, systemFactory, _, _) =>
            {
                return systemConfigurations
                    .Select(scs => systemFactory.Create(host, scs, Root, Details))
                    .ToArray();
            };

            return this;
        }

        public IHostConfiguration Use(Action<string> output)
        {
            Output = output;
            return this;
        }

        /// <inheritdoc />
        public IHostConfiguration Use(params IHostExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException("No extensions specified", nameof(extensions));
            }

            Extensions = extensions
                .Concat(Extensions)
                .Distinct()
                .ToArray();
            return this;
        }

        /// <inheritdoc />
        public IHostConfiguration Use(bool useWrapper)
        {
            UseWrapper = useWrapper;

            return this;
        }
    }
}
