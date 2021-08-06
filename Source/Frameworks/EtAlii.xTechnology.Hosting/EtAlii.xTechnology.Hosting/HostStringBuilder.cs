// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Net;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;

    public class HostStringBuilder
    {
        public HostString Build(IConfigurationSection configuration, IModule parentModule, IPAddress fallbackAddress)
        {
            // Port magic.
            int? port = configuration.GetValue("Port", -1);
            if (parentModule != null)
            {
                port = port == -1
                    ? parentModule.HostString.Port
                    : port;
            }

            // Host magic.
            var ipAddressString = configuration.GetValue<string>("IpAddress", null);
            string host = null;
            if (!string.IsNullOrWhiteSpace(ipAddressString))
            {
                if (IPAddress.TryParse(ipAddressString, out var ipAddress))
                {
                    host = ipAddress.ToString();
                }
                else
                {
                    throw new InvalidOperationException($"Unable to parse {GetType().Name} IP address: {ipAddressString}");
                }
            }

            host = port != -1 && host != null
                ? host
                : fallbackAddress.ToString();

            if (parentModule != null)
            {
                host = Equals(host, IPAddress.None.ToString())
                    ? IPAddress.Parse(parentModule.HostString.Host).ToString()
                    : host;
            }

            var hostString = port.HasValue && port != -1
                ? new HostString(host, port.Value)
                : new HostString(host);
            return hostString;

        }
    }
}
