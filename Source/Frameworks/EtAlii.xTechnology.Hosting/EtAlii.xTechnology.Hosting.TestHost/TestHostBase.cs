// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Threading.Tasks;
    using Serilog;

    public class TestHostBase<THostManager> : HostBase
        where THostManager: IHostManager, new()
    {
        private readonly ILogger _logger = Log.ForContext<TestHostBase<THostManager>>();

        protected new THostManager Manager => (THostManager)base.Manager;

        protected TestHostBase(IHostOptions options, ISystemManager systemManager)
            : base(options, systemManager)
        {
        }

        public override void Setup(ICommand[] commands, Status[] status)
        {
            _logger.Information("Setting up host {HostName}", GetType().Name);
            var manager = new THostManager();
            base.Setup(commands, status, manager);
        }

        protected override Task OnStartException(Exception exception)
        {
            _logger.Fatal(exception, "Exception starting hosting {HostName}", GetType().Name);
            return base.OnStartException(exception);
        }

        protected override void OnStopException(Exception exception)
        {
            _logger.Fatal(exception, "Exception stopping hosting {HostName}", GetType().Name);
            base.OnStopException(exception);
        }

        public override async Task Start()
        {
            _logger.Information("Starting host {HostName}", GetType().Name);
            await base.Start().ConfigureAwait(false);
        }

        public override async Task Stop()
        {
            _logger.Information("Stopping host {HostName}", GetType().Name);
            await base.Stop().ConfigureAwait(false);
        }

        public override async Task Shutdown()
        {
            _logger.Information("Shutting down host {HostName}", GetType().Name);
            await base.Shutdown().ConfigureAwait(false);
        }


        public override void Initialize()
        {
            _logger.Information("Initializing host {HostName}", GetType().Name);
            base.Initialize();
        }

    }
}
