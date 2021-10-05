// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class TrayIconHost
    {
        protected override Task Started() => Task.CompletedTask;

        protected override Task Stopping() => Task.CompletedTask;

        public static void Start(HostOptions options)
        {
            var arguments = Environment.GetCommandLineArgs();
            for(var i = 0; i < arguments.Length; i++)
            {
                if (arguments[i] == "-d" && i + 1 < arguments.Length)
                {
                    var delay = int.Parse(arguments[i + 1]);
                    Task.Delay(delay).Wait();
                }
            }

            var host = Factory.Create<IHost>(options);
            // Start hosting both the infrastructure and the storage.
            host.Start();
        }
    }
}
