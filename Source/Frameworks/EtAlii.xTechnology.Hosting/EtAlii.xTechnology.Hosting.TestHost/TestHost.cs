// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Server;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Hosting;
    using System.Net.Http;
    using Serilog;

    // This class is specific enough. We'll keep it's naming
#pragma warning disable CA1724
    public abstract class TestHost : HostBase, ITestHost
#pragma warning restore CA1724
    {
        private IDisposable _server;
        private TestServer _testServer;

        private readonly ILogger _logger = Log.ForContext<TestHost>();

        protected TestHost(IHostOptions options)
            : base(options)
        {
        }

        public override void Setup(ICommand[] commands, Status[] status)
        {
            _logger.Information("Setting up host {HostName}", GetType().Name);
            base.Setup(commands, status);
        }

        /// <summary>
        /// In case of testing we really need to take ownership on how the
        /// host and web application configuration is done.
        /// </summary>
        /// <returns></returns>
        protected override Microsoft.Extensions.Hosting.IHost CreateHost()
        {
            return Microsoft.Extensions.Hosting.Host
                .CreateDefaultBuilder()
                .ConfigureServices(ConfigureBackgroundServices)
                .ConfigureHostConfiguration(ConfigureHostConfiguration)
                .ConfigureWebHost(webHostBuilder =>
                {
                    webHostBuilder
                        .UseTestServer()
                        .Configure(ConfigureApplication);
                })
                .Build();
        }

        protected override async Task Starting()
        {
            _logger.Information("Starting host {HostName}", GetType().Name);
            await base.Starting().ConfigureAwait(false);
        }

        protected override Task Started()
        {
            _logger.Information("Started host {HostName}", GetType().Name);

            _testServer = Host.GetTestServer();
            _testServer.PreserveExecutionContext = false;
            _testServer.AllowSynchronousIO = true;
            _server = (IDisposable)Host.Services.GetService(typeof(IServer));

            _logger.Information("Test server acquired");

            return Task.CompletedTask;
        }

        protected override Task Stopping()
        {
            _logger.Information("Stopping host {HostName}", GetType().Name);

            if (_testServer != null)
            {
                _testServer.Dispose();
                _testServer = null;
            }

            if (_server != null)
            {
                _server.Dispose();
                _server = null;
            }

            _logger.Information("Test server removed");

            return Task.CompletedTask;
        }

        protected override async Task Stopped()
        {
            await base.Stopped().ConfigureAwait(false);
            _logger.Information("Stopped host {HostName}", GetType().Name);
        }

        public override async Task Shutdown()
        {
            _logger.Information("Shutting down host {HostName}", GetType().Name);
            await base.Shutdown().ConfigureAwait(false);
        }

        public HttpMessageHandler CreateHandler() => _testServer.CreateHandler();

        public HttpClient CreateClient() => _testServer.CreateClient();
    }
}
