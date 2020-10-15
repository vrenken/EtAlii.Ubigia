namespace EtAlii.xTechnology.Hosting
{
	using System;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Server.Kestrel.Core;
	using Microsoft.Extensions.Logging;

	public abstract class HostManagerBase : IHostManager, INotifyPropertyChanged
	{
	    private IWebHostBuilder _hostBuilder;

	    public IWebHost Host { get; private set; }

		public event PropertyChangedEventHandler PropertyChanged;

	    public event Action<IApplicationBuilder> ConfigureApplication;
	    public event Action<IWebHostBuilder> ConfigureHost;
	    public event Action<KestrelServerOptions> ConfigureKestrel;

		public bool ShouldOutputLog { get => _shouldOutputLog; set => PropertyChanged.SetAndRaise(this, ref _shouldOutputLog, value); }
        private bool _shouldOutputLog;

        public LogLevel LogLevel { get => _logLevel; set => PropertyChanged.SetAndRaise(this, ref _logLevel, value); }
        private LogLevel _logLevel = LogLevel.Information;

        private readonly Status _status;

        protected HostManagerBase()
        {
	        _status = new Status(GetType().Name);

			PropertyChanged += OnPropertyChanged;
            UpdateStatus();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateStatus();
        }

        [SuppressMessage("Sonar Code Smell", "S4792:Configuring loggers is security-sensitive", Justification = "Safe to do so here.")]
        private IWebHostBuilder CreateHostBuilder()
        {
	        return WebHost.CreateDefaultBuilder()
		        .UseKestrel(OnConfigureKestrel)
		        .ConfigureLogging(logging =>
		        {
			        if (!Debugger.IsAttached) return;

			        // SonarQube: Make sure that this logger's configuration is safe.
			        // I think it is as this host is for testing only.
			        logging.AddDebug();
					
			        logging.AddFilter(level => ShouldOutputLog && level >= LogLevel);
			        logging.AddFilter("Microsoft.AspNetCore.SignalR", level => ShouldOutputLog && level >= LogLevel);
			        logging.AddFilter("Microsoft.AspNetCore.Http.Connections", level => ShouldOutputLog && level >= LogLevel);
			        logging.SetMinimumLevel(LogLevel.Trace);
		        })
		        .Configure(OnConfigureApplication);
        }
	    
	    protected abstract IWebHost CreateHost(IWebHostBuilder webHostBuilder, out bool hostIsAlreadyStarted);

		public virtual Task Starting()
        {
	        _hostBuilder = CreateHostBuilder();
	        return Task.CompletedTask;
        }

	    public virtual async Task Started()
	    {
		    ConfigureHost?.Invoke(_hostBuilder);

		    Host = CreateHost(_hostBuilder, out var hostIsAlreadyStarted);
		    if (!hostIsAlreadyStarted)
		    {
			    await Host.StartAsync();
		    }
	    }

		public virtual Task Stopping()
        {
	        return Task.CompletedTask;
        }

	    public virtual async Task Stopped()
	    {
		    await Host.StopAsync(TimeSpan.FromMinutes(1));
		    Host = null;
	    }

		public void Setup(ref ICommand[] commands, ref Status[] status, IHost host)
        {
            commands = commands
                .Concat(new ICommand[]
                {
                    new ToggleLogOutputCommand(host, this),
                    new IncreaseLogLevelCommand(host, this),
                    new DecreaseLogLevelCommand(host, this),
                })
                .ToArray();
            status = status
                .Concat(new[]
                {
                    _status
                })
                .ToArray();
        }

		public virtual void Initialize() { }

		private void UpdateStatus()
        {
            _status.Title = ".NET Core Host";
            var sb = new StringBuilder();
            sb.AppendLine($"Log output: {(ShouldOutputLog ? "Enabled" : "Disabled")}");
            sb.AppendLine($"Log level: {LogLevel}");
            _status.Summary = _status.Description = sb.ToString();

        }

		private void OnConfigureKestrel(WebHostBuilderContext context, KestrelServerOptions options) => ConfigureKestrel?.Invoke(options);
		private void OnConfigureApplication(WebHostBuilderContext context, IApplicationBuilder configurationBuilder) => ConfigureApplication?.Invoke(configurationBuilder);
    }
}