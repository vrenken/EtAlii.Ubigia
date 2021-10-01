// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.Configuration;

    public class HostOptions : IHostOptions
    {
        /// <inheritdoc />
        public IConfigurationRoot ConfigurationRoot { get; }

        public string EnabledImage { get; private set; }

        public string ErrorImage { get; private set; }

        public string DisabledImage { get; private set; }

        public ICommand[] Commands { get; private set; }

        public string HostTitle { get; private set; }

        public string ProductTitle { get; private set; }

        /// <inheritdoc />
        public bool UseWrapper { get; private set; }

        /// <inheritdoc />
        public Func<IHost> CreateHost { get; private set; }

        public Action<string> Output { get; private set; }

        /// <inheritdoc />
        public IHostExtension[] Extensions { get; private set; }

        /// <inheritdoc />
        public ConfigurationDetails Details { get; private set; }

        public HostOptions(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
            Extensions = Array.Empty<IHostExtension>();
            Commands = Array.Empty<ICommand>();
        }

        /// <inheritdoc />
        public IHostOptions Use(ConfigurationDetails details)
        {
            Details = details;

            return this;
        }

        public IHostOptions Use(string enabledImage, string errorImage, string disabledImage)
        {
            EnabledImage = enabledImage;
            ErrorImage = errorImage;
            DisabledImage = disabledImage;

            return this;
        }

        public IHostOptions Use(string productTitle = "EtAlii.xTechnology.Hosting", string hostTitle = "Host")
        {
            ProductTitle = productTitle;
            HostTitle = hostTitle;

            return this;
        }

        public IHostOptions Use(params ICommand[] commands)
        {
            Commands = Commands
                .Concat(commands)
                .Distinct()
                .ToArray();

            return this;
        }

        /// <inheritdoc />
        public IHostOptions Use(Func<IHost> createHost)
        {
            if (CreateHost != null)
            {
                throw new InvalidOperationException("Host factory configuration already done");
            }

            CreateHost = createHost;

            return this;
        }

        public IHostOptions Use(Action<string> output)
        {
            Output = output;
            return this;
        }

        /// <inheritdoc />
        public IHostOptions Use(params IHostExtension[] extensions)
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
        public IHostOptions Use(bool useWrapper)
        {
            UseWrapper = useWrapper;

            return this;
        }
    }
}
