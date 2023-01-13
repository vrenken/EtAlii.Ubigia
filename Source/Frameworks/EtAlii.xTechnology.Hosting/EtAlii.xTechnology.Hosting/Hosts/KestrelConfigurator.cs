// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Serilog;

public class KestrelConfigurator
{
    private readonly ILogger _logger = Log.ForContext<KestrelConfigurator>();

    public void Configure(
        KestrelServerOptions kestrelOptions,
        IService[] services,
        IConfigurationRoot configurationRoot)
    {
        var networkServiceConfigurations = services
            .OfType<INetworkService>()
            .Select(networkService => networkService.Configuration)
            .ToArray();
        foreach (var networkServiceConfiguration in networkServiceConfigurations)
        {
            ConfigureKestrelForService(kestrelOptions, networkServiceConfiguration.IpAddress, (int)networkServiceConfiguration.Port, configurationRoot);
        }

        kestrelOptions.ConfigureHttpsDefaults(options => options.SslProtocols = SslProtocols.Tls13);
        kestrelOptions.Limits.MaxRequestBodySize = 1024 * 1024 * 2;
        kestrelOptions.Limits.MaxRequestBufferSize = 1024 * 1024 * 2;
        kestrelOptions.Limits.MaxResponseBufferSize = 1024 * 1024 * 2;
        kestrelOptions.AllowSynchronousIO = true;
    }

    [ SuppressMessage(
        category: "Sonar Code Smell",
        checkId: "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields",
        Justification = "Safe to do so here, this is a patch to get Kestrel to work as needed.")]
    private void ConfigureKestrelForService(
        KestrelServerOptions options,
        string ipAddressString,
        int port,
        IConfigurationRoot configurationRoot)
    {
        if (!IPAddress.TryParse(ipAddressString, out var ipAddress))
        {
            _logger.Error("Unable to parse {IpAddress} for configuration of Kestrel", ipAddressString ?? "NULL");
        }
        else
        {
            _logger.Debug("Parsed {IpAddress} for configuration of Kestrel", ipAddress);
        }
        var property = options.GetType().GetProperty("ListenOptions", BindingFlags.NonPublic | BindingFlags.Instance);

        var listenOptions = property!.GetValue(options) as IEnumerable<ListenOptions>;
        if (listenOptions!.Any(lo => Equals(lo.IPEndPoint!.Address, ipAddress) && lo.IPEndPoint.Port == port)) return;

        if (Equals(ipAddress, IPAddress.None))
        {
            options.ListenAnyIP(port, lo => OnConfigureListenOptions(lo, configurationRoot));
        }
        else if (Equals(ipAddress, IPAddress.Loopback))
        {
            options.ListenLocalhost(port, lo => OnConfigureListenOptions(lo, configurationRoot));
        }
        else
        {
            options.Listen(ipAddress!, port, lo => OnConfigureListenOptions(lo, configurationRoot));
        }
    }

    private void OnConfigureListenOptions(ListenOptions options, IConfigurationRoot configurationRoot)
    {
        var configuration = configurationRoot.GetSection("Host").Get<HostConfiguration>();
        if (string.IsNullOrWhiteSpace(configuration.CertificateFile) || string.IsNullOrWhiteSpace(configuration.CertificatePassword))
        {
            _logger.Information("No HTTPS certificate specified for {EndPoint} - Using defaults", options.EndPoint.ToString());
            options.UseHttps();
        }
        else if (!File.Exists(configuration.CertificateFile))
        {
            throw new InvalidOperationException($"Certificate file not found: {configuration.CertificateFile}");
        }
        else
        {
            _logger.Information("Using {CertificateFile} instead of HTTPS defaults for {EndPoint}", configuration.CertificateFile, options.EndPoint.ToString());
            options.UseHttps(configuration.CertificateFile, configuration.CertificatePassword);
        }
        // We want all the communication to use both HTTP2 and HTTPS. In the future well move everything to HTTP3.
        options.Protocols = HttpProtocols.Http2;
    }
}
