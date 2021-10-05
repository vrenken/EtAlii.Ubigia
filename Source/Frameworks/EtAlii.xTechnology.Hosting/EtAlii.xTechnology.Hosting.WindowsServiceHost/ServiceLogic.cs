// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable UnassignedGetOnlyAutoProperty
namespace EtAlii.xTechnology.Hosting
{
    using System.Collections.Generic;
    using EtAlii.xTechnology.MicroContainer;

    public class ServiceLogic : IServiceLogic
    {
        // SERVINFSERV$SQLEXPRESS
        public string Name { get; }

        public string DisplayName { get; }

        public string Description { get; }

        private readonly IHost _host;

        public ServiceLogic(HostOptions options)
        {
            _host = Factory.Create<IHost>(options);
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
