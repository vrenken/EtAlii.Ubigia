// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Service.Grpc
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.Configuration;

    public abstract class GrpcServiceBase<THost, TSystem> : ServiceBase<THost, TSystem>
        where THost : class, IHost
        where TSystem : class, ISystem
    {
        protected GrpcServiceBase(IConfigurationSection configuration)
            : base(configuration)
        {
        }

        public override async Task Start()
        {
            await base.Start().ConfigureAwait(false);
            var host = (IConfigurableHost) Host;
            host.ConfigureKestrel += OnConfigureKestrel;
        }

        public override async Task Stop()
        {
            var host = (IConfigurableHost) Host;
            host.ConfigureKestrel -= OnConfigureKestrel;
            await base.Stop().ConfigureAwait(false);
        }

        
        [ SuppressMessage("Sonar Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields", Justification = "Safe to do so here.")]
        private void OnConfigureKestrel(KestrelServerOptions options)
        {
            if (!HostString.Port.HasValue)
            {
                throw new InvalidOperationException("Unable to configure Grpc service: No port specified.");
            }

            var ipAddress = IPAddress.Parse(HostString.Host);

            var property = options.GetType().GetProperty("ListenOptions", BindingFlags.NonPublic | BindingFlags.Instance);

            var listenOptions = property!.GetValue(options) as IEnumerable<ListenOptions>;
            if (listenOptions!.Any(lo => Equals(lo.IPEndPoint.Address, ipAddress) && lo.IPEndPoint.Port == HostString.Port)) return;
            
            if (Equals(ipAddress, IPAddress.None))
            {
                options.ListenAnyIP(HostString.Port.Value, OnConfigureListenOptions);
            }
            else if (Equals(ipAddress, IPAddress.Loopback))
            {
                options.ListenLocalhost(HostString.Port.Value, OnConfigureListenOptions);
            }
            else
            {
                options.Listen(ipAddress, HostString.Port.Value, OnConfigureListenOptions);
            }
        }
        
        protected virtual void OnConfigureListenOptions(ListenOptions options)
        {
            // Override this method to configure any additional listen options.
            options.Protocols = HttpProtocols.Http2;
            // options.UseHttps("<path to .pfx file>", "<certificate password>")
        }
    }

    public abstract class GrpcServiceBase : GrpcServiceBase<IHost, ISystem>
    {
        protected GrpcServiceBase(IConfigurationSection configuration) : base(configuration)
        {
        }
    }
}