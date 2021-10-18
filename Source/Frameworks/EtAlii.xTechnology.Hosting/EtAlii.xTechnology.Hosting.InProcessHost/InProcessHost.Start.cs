// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class InProcessHost
    {
        public static async Task Start(HostOptions options)
        {
            var host = Factory.Create<InProcessHost>(options);

            // Start hosting both the infrastructure and the storage.
            await host
                .Start()
                .ConfigureAwait(false);

            // ReSharper disable once UnusedVariable
            #pragma warning disable S1481
            var control = new HostControl(host);
            #pragma warning restore S1481
        }
    }
}
