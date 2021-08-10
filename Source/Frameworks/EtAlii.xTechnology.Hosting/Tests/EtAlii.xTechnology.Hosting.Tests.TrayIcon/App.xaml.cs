// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.TrayIcon
{
	using System.Windows;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting.Diagnostics;
	using Microsoft.Extensions.Configuration;

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
        private async void OnApplicationStartup(object sender, StartupEventArgs e)
        {
	        var details = await new ConfigurationDetailsParser()
                .Parse("settings.json")
                .ConfigureAwait(false);

	        var configurationRoot = new ConfigurationBuilder()
		        .AddConfigurationDetails(details)
                .AddConfiguration(DiagnosticsOptions.ConfigurationRoot) // For testing we'll override the configured logging et.
		        .Build();

	        var hostOptions = new HostOptionsBuilder()
		        .Build(configurationRoot, details)
                .UseTrayIconHost(this,
                    "Icon-Logo-White-Shaded.ico",
                    "Icon-Logo-Black.ico",
                    "Icon-Logo-Red.ico")
                .UseHostDiagnostics();

	        TrayIconHost.Start(hostOptions);
        }
    }
}
