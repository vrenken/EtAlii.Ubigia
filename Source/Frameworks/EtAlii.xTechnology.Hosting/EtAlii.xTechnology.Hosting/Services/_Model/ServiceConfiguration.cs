// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Serilog;

    public class ServiceConfiguration
    {
        public IConfigurationSection Section { get; private set; }
        public IConfigurationRoot Root { get; private set; }

        public string Factory { get; init; }
        public string IpAddress { get; init; }
        public uint Port { get; init; }
        public string Path { get; init; }

        public static bool TryCreate(
            IConfigurationSection configurationSection,
            IConfigurationRoot configurationRoot,
            out ServiceConfiguration configuration)
        {
            var log = Log.ForContext<ServiceConfiguration>();

            log.Verbose("Trying to create ServiceConfiguration for {ConfigurationSectionPath}", configurationSection.Path);
            try
            {
                configuration = configurationSection.Get<ServiceConfiguration>();
                if (configuration != null)
                {
                    configuration.Section = configurationSection;
                    configuration.Root = configurationRoot;
                    log.Verbose("Created ServiceConfiguration for {ConfigurationSectionPath}", configurationSection.Path);
                    return true;
                }
                log.Verbose("Unable to create ServiceConfiguration for {ConfigurationSectionPath}", configurationSection.Path);
                return false;
            }
            catch (InvalidOperationException e)
            {
                log.Error(e, "Error creating ServiceConfiguration for {ConfigurationSectionPath}", configurationSection.Path);
                configuration = null;
                return false;
            }
        }
    }
}
