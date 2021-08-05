// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable UnassignedGetOnlyAutoProperty
namespace EtAlii.xTechnology.Hosting
{
    using System.Collections.Generic;

    public class ServiceLogic : IServiceLogic
    {
        // SERVINFSERV$SQLEXPRESS
        public string Name { get; }

        public string DisplayName { get; }

        public string Description { get; }

        private readonly IHost _host;

        public ServiceLogic(IHostOptions options)
        {
            _host = new HostFactory<WindowsServiceHost>().Create(options);
        }


        public void Start(IEnumerable<string> args)
        {
            _host.Start();
        }

        public void Stop()
        {
            _host.Stop();
        }
    }
}
