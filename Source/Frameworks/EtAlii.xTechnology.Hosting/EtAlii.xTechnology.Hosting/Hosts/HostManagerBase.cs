// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Server.Kestrel.Core;

	public abstract class HostManagerBase : IHostManager
	{
	    private IWebHostBuilder _hostBuilder;

	    public IWebHost Host { get; private set; }

	    public event Action<IApplicationBuilder> ConfigureApplication;
	    public event Action<IWebHostBuilder> ConfigureHost;
	    public event Action<KestrelServerOptions> ConfigureKestrel;
        private IWebHostBuilder CreateHostBuilder()
        {
	        return WebHost.CreateDefaultBuilder()
		        .UseKestrel(OnConfigureKestrel)
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
			    await Host
                    .StartAsync()
                    .ConfigureAwait(false);
		    }
	    }

		public virtual Task Stopping()
        {
	        return Task.CompletedTask;
        }

	    public virtual async Task Stopped()
	    {
            if (Host != null)
            {
                await Host
                    .StopAsync(TimeSpan.FromMinutes(1))
                    .ConfigureAwait(false);
                Host = null;
            }
	    }

		public void Setup(ref ICommand[] commands, IHost host)
        {
            commands = commands
                .Concat(new ICommand[]
                {
                    new ToggleLogOutputCommand(host),
                    new IncreaseLogLevelCommand(host),
                    new DecreaseLogLevelCommand(host),
                })
                .ToArray();
        }

		public virtual void Initialize() { }

		private void OnConfigureKestrel(WebHostBuilderContext context, KestrelServerOptions options) => ConfigureKestrel?.Invoke(options);
		private void OnConfigureApplication(WebHostBuilderContext context, IApplicationBuilder configurationBuilder) => ConfigureApplication?.Invoke(configurationBuilder);
    }
}
