// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Polly;

    public abstract class HostBase : IConfigurableHost
    {
        IHostManager IConfigurableHost.Manager => _manager;
        protected IHostManager Manager => _manager;
        private IHostManager _manager;

        public event Action<IApplicationBuilder> ConfigureApplication;
        public event Action<IWebHostBuilder> ConfigureHost;
        public event Action<KestrelServerOptions> ConfigureKestrel;
        public State State { get => _state; protected set => PropertyChanged.SetAndRaise(this, ref _state, value); }
        private State _state;

        public Status[] Status => _status;
        private Status[] _status = Array.Empty<Status>();

	    private readonly ISystemManager _systemManager;

	    public ISystem[] Systems => _systemManager.Systems;

        public ICommand[] Commands => _commands;
        private ICommand[] _commands;

        public IHostOptions Options { get; }

        public event PropertyChangedEventHandler PropertyChanged;


        private readonly Status _selfStatus;

        protected HostBase(IHostOptions options, ISystemManager systemManager)
        {
            Options = options;
	        _systemManager = systemManager;

            _selfStatus = new Status(GetType().Name);

            PropertyChanged += OnPropertyChanged;
            UpdateStatus();
        }


        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateStatus();
        }

        protected virtual Task Starting() => _manager.Starting();

        protected virtual Task Started() => _manager.Started();

        protected virtual Task Stopping() => _manager.Stopping();

        protected virtual Task Stopped() => _manager.Stopped();

        public virtual void Setup(ICommand[] commands, Status[] status)
        {
            var manager = new HostManager();
            Setup(commands, status, manager);
        }

        protected void Setup(ICommand[] commands, Status[] status, IHostManager manager)
        {
            _commands = commands;
            _manager = manager;
            _manager.Setup(ref commands, this);
            _manager.ConfigureApplication += builder => ConfigureApplication?.Invoke(builder);
            _manager.ConfigureHost += builder => ConfigureHost?.Invoke(builder);
            _manager.ConfigureKestrel += options => ConfigureKestrel?.Invoke(options);

            _status = status
                .Concat(new [] {_selfStatus} )
                .ToArray();
            _commands = commands;
            foreach (var s in _status)
            {
                s.PropertyChanged += OnStatusPropertyChanged;
            }
        }

        public virtual void Initialize()
        {
            _manager.Initialize();

            foreach (var system in Systems)
            {
                system.Initialize();
            }
        }

        protected virtual async Task OnStartException(Exception exception)
        {
            Trace.WriteLine($"Fatal exception in hosting: {exception}");
            Trace.WriteLine("Unable to start hosting");
            State = State.Error;
            await Task.Delay(100).ConfigureAwait(false);
            await Stop().ConfigureAwait(false);
        }

        public virtual async Task Start()
        {
            State = State.Starting;

            await Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    5,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, _, _) => OnStartException(exception)
                )
                .ExecuteAsync(async () =>
                {
                    await Starting().ConfigureAwait(false);
	                await _systemManager.Start().ConfigureAwait(false);
                    State = State.Running;
                    await Started().ConfigureAwait(false);
                })
                .ConfigureAwait(false);
        }

        protected virtual void OnStopException(Exception exception)
        {
            State = State.Error;
            Trace.WriteLine($"Fatal exception in hosting: {exception}");
            Trace.WriteLine("Unable to stop hosting");
        }

        public virtual async Task Stop()
        {
            State = State.Stopping;

            await Policy
                .Handle<Exception>()
                .WaitAndRetry(
                    5,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, _, _) => OnStopException(exception)
                )
                .Execute(async () =>
                {
                    await Stopping().ConfigureAwait(false);
	                await _systemManager.Stop().ConfigureAwait(false);
                    State = State.Stopped;
                    await Stopped().ConfigureAwait(false);
                })
                .ConfigureAwait(false);
        }

        public virtual async Task Shutdown()
        {
            await Stop().ConfigureAwait(false);

            State = State.Shutdown;
        }

        private void OnStatusPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status)));
        }

        private void UpdateStatus()
        {
            _selfStatus.Title = ".NET Core Host";
            var sb = new StringBuilder();
            sb.AppendLine($"State: {State}");
            _selfStatus.Summary = _selfStatus.Description = sb.ToString();
        }
    }
}
