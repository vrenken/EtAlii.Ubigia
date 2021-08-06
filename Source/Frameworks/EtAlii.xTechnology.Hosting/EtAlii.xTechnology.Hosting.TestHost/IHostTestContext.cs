// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;

    public interface IHostTestContext
    {
        /// <summary>
        /// A root configuration instance that should be used to
        /// initialize any host-side systems.
        /// </summary>
        IConfigurationRoot HostConfiguration { get; }

        /// <summary>
        /// A root configuration instance that should be used to
        /// initialize any client-side systems.
        /// </summary>
        IConfigurationRoot ClientConfiguration { get; }

        Task Start(PortRange portRange);

        Task Stop();

        HttpMessageHandler CreateHandler();
        HttpClient CreateClient();
    }
}
