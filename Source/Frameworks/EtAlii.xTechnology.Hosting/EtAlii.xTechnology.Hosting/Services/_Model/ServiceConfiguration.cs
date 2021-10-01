// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using Microsoft.Extensions.Configuration;

    public class ServiceConfiguration
    {
        public IConfigurationSection Section { get; private set; }
        public IConfigurationRoot Root { get; private set; }

        public IConfigurationDetails Details { get; private set; }
        public string Factory { get; init; }
        public string IpAddress { get; init; }
        public uint Port { get; init; }
        public string Path { get; init; }

        public static bool TryCreate(
            IConfigurationSection configurationSection,
            IConfigurationRoot configurationRoot,
            IConfigurationDetails configurationDetails,
            out ServiceConfiguration configuration)
        {
            try
            {
                configuration = configurationSection.Get<ServiceConfiguration>();
                if (configuration != null)
                {
                    configuration.Section = configurationSection;
                    configuration.Root = configurationRoot;
                    configuration.Details = configurationDetails;
                    return true;
                }
                return false;
            }
            catch (InvalidOperationException)
            {
                configuration = null;
                return false;
            }
        }
    }
}
