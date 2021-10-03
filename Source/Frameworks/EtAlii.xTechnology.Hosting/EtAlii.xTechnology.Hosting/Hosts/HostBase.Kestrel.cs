// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Security.Authentication;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;

    public abstract partial class HostBase
    {

        private void ConfigureKestrel(KestrelServerOptions kestrelOptions)
        {
            var networkServices = Services.OfType<INetworkService>().ToArray();
            foreach (var networkService in networkServices)
            {
                ConfigureKestrelForService(kestrelOptions, networkService.Configuration.IpAddress, (int)networkService.Configuration.Port);
            }

            kestrelOptions.ConfigureHttpsDefaults(options => options.SslProtocols = SslProtocols.Tls13);
            kestrelOptions.Limits.MaxRequestBodySize = 1024 * 1024 * 2;
            kestrelOptions.Limits.MaxRequestBufferSize = 1024 * 1024 * 2;
            kestrelOptions.Limits.MaxResponseBufferSize = 1024 * 1024 * 2;
            kestrelOptions.AllowSynchronousIO = true;
        }

        [ SuppressMessage(
            category: "Sonar Code Smell",
            checkId: "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields",
            Justification = "Safe to do so here, this is a patch to get Kestrel to work as needed.")]
        private void ConfigureKestrelForService(KestrelServerOptions options, string ipAddressString, int port)
        {
            var ipAddress = IPAddress.Parse(ipAddressString);

            var property = options.GetType().GetProperty("ListenOptions", BindingFlags.NonPublic | BindingFlags.Instance);

            var listenOptions = property!.GetValue(options) as IEnumerable<ListenOptions>;
            if (listenOptions!.Any(lo => Equals(lo.IPEndPoint.Address, ipAddress) && lo.IPEndPoint.Port == port)) return;

            if (Equals(ipAddress, IPAddress.None))
            {
                options.ListenAnyIP(port, OnConfigureListenOptions);
            }
            else if (Equals(ipAddress, IPAddress.Loopback))
            {
                options.ListenLocalhost(port, OnConfigureListenOptions);
            }
            else
            {
                options.Listen(ipAddress, port, OnConfigureListenOptions);
            }
        }

        private void OnConfigureListenOptions(ListenOptions options)
        {
            // We want all the communication to use both HTTP2 and HTTPS. In the future well move everything to HTTP3.
            options.Protocols = HttpProtocols.Http2;
            options.UseHttps();
            // options.UseHttps("<path to .pfx file>", "<certificate password>")
        }
    }
}
