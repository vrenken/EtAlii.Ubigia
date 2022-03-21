// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.TestHost;
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

        /// <summary>
        /// Create a HttpMessageHandler.
        /// </summary>
        /// <returns></returns>
        HttpMessageHandler CreateHandler();

        /// <summary>
        /// Create a websocket client.
        /// </summary>
        /// <returns></returns>
        WebSocketClient CreateWebSocketClient();

        /// <summary>
        /// Create a traditional HTTP (REST) client.
        /// </summary>
        /// <returns></returns>
        HttpClient CreateClient();
    }
}
