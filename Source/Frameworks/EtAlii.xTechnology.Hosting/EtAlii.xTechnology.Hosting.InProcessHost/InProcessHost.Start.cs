// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class InProcessHost
    {
        public static void Start(IHostOptions options)
        {
            var host = new HostFactory<InProcessHost>().Create(options);

            // Start hosting both the infrastructure and the storage.
            host.Start();

            // ReSharper disable once UnusedVariable
            #pragma warning disable S1481
            var control = new HostControl(host);
            #pragma warning restore S1481
        }
    }
}