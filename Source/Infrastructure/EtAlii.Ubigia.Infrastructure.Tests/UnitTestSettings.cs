// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

using Xunit;
using System.IO;
using System.Text;
using System.Runtime.CompilerServices;
using EtAlii.xTechnology.Diagnostics;
using Microsoft.Extensions.Configuration;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly, DisableTestParallelization = false)]

// ReSharper disable CheckNamespace
internal static class UnitTestConstants
{
    public const int NetworkPortRangeStart = 20000;
}

internal static class LoggingInitializer
{
    [ModuleInitializer]
    internal static void Initialize()
    {
        // The diagnostics subsystem in the unit tests also requires access to a settings configuration root.
        // But in contrast to the default configuration root this instance does not require advanced parsing.

        // We cannot apply using to this stream as the builder is going to own it.
#pragma warning disable CA2000
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(DiagnosticsSettingsJson));
        var configurationRoot = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();
#pragma warning restore CA2000

        DiagnosticsConfiguration.Initialize(typeof(LoggingInitializer).Assembly, configurationRoot);
    }

    private static string DiagnosticsSettingsJson =
    @"{
        ""Logging"": {
            ""LogLevel"": {
                ""Default"": ""Warning""
            },
            ""Output"": {
                ""Seq"": {
                    ""Enabled"": true,
                    ""Address"": ""http://seq.avalon:5341"",
                    ""LogLevel"": ""Debug""
                },
            ""Console"": {
                ""Enabled"": false,
                ""LogLevel"": ""Info""
            }
        }
    }";
}

