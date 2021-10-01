// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Net;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;

    public class HostStringBuilder
    {
        public HostString Build(IConfigurationSection configuration, IPAddress fallbackAddress)
        {
            // Port magic.
            var port = configuration.GetValue("Port", -1);

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

            var hostString = port != -1
                ? new HostString(host, port)
                : new HostString(host);
            return hostString;

        }
    }
}
