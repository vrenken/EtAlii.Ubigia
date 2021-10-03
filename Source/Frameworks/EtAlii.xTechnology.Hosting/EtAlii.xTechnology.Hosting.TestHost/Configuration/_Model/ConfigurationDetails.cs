// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class ConfigurationDetails : IConfigurationDetails
    {
        public ReadOnlyDictionary<string, string> Folders { get; }
        public ReadOnlyDictionary<string, string> Hosts { get; }
        public ReadOnlyDictionary<string, int> Ports { get; }
        public ReadOnlyDictionary<string, string> Paths { get; }

        public string Configuration { get; }

        public ConfigurationDetails(IDictionary<string, string> folders, IDictionary<string, string> hosts, IDictionary<string, int> ports, IDictionary<string, string> paths, string configuration)
        {
            Configuration = configuration;
            Folders = new ReadOnlyDictionary<string, string>(folders);
            Hosts = new ReadOnlyDictionary<string, string>(hosts);
            Ports = new ReadOnlyDictionary<string, int>(ports);
            Paths = new ReadOnlyDictionary<string, string>(paths);
        }
    }
}
