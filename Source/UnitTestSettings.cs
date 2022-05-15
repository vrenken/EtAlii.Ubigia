// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

using Xunit;
using System.IO;
using System.Text;
using System.Runtime.CompilerServices;
using EtAlii.xTechnology.Diagnostics;
using Microsoft.Extensions.Configuration;

// We want to run as much tests (unit and integration ones) in parallel.
[assembly: CollectionBehavior(CollectionBehavior.CollectionPerClass, DisableTestParallelization = false)]

// ReSharper disable once PartialTypeWithSinglePart
internal static partial class UnitTestSettings
{
    public const int NetworkPortRangeStart = 20000;
}

internal static class TestModuleInitializer
{
    [ModuleInitializer]
    internal static void Initialize()
    {
        // The diagnostics subsystem in the unit tests also requires access to a settings configuration root.
        // But in contrast to the default configuration root this instance does not require advanced parsing.

#pragma warning disable CA2000
        // We cannot use the using statement to dispose this stream as the builder is going to own it.
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(DiagnosticsSettingsJson));
        var configurationRoot = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();
#pragma warning restore CA2000

        DiagnosticsOptions.Initialize(typeof(TestModuleInitializer).Assembly, configurationRoot);
    }

    private static string DiagnosticsSettingsJson =
    @"{
        ""Serilog"": {
            ""MinimumLevel"": {
                ""Default"": ""Verbose"",
                ""Override"": {
                    ""Microsoft"": ""Information"",
                    ""Microsoft.AspNetCore"": ""Information"",
                    ""Microsoft.AspNetCore.SignalR"": ""Information"",
                    ""Microsoft.AspNetCore.Http.Connections"": ""Information""
                }
            },
            ""WriteTo"": [
                {
                    ""Name"": ""Async"",
                    ""Args"": {
                        ""configure"": [
                            {
                                ""Name"": ""Console"",
                                ""Args"": {
                                    ""restrictedToMinimumLevel"": ""Information"",
                                    ""outputTemplate"": ""{Level:u3}{Timestamp: [HH:mm:ss]}: {Message}{NewLine}""
                                }
                            },
                            {
                                ""Name"": ""Seq"",
                                ""Args"": {
                                    ""#COMMENT1"": ""// If you want to have a local Seq centralized logging instance please start a local seq instance in docker using the commandline below"",
                                    ""#COMMENT2"": ""// docker run --name seq -e ACCEPT_EULA=Y -p 5341:80 datalust/seq:latest"",
                                    ""#COMMENT3"": ""// It can then be accessed using the following URL:"",
                                    ""#COMMENT4"": ""// http://127.0.0.1:5341"",
                                    ""#COMMENT5"": ""// If your Seq logging instance runs on another machine you can use the following"",
                                    ""#COMMENT6"": ""// commandline in an elevated PowerShell console to forward the local port to another system:"",
                                    ""#COMMENT7"": ""// netsh interface portproxy add v4tov4 listenaddress=127.0.0.1 listenport=5341 connectaddress=OTHERIP connectport=5341"",
                                    ""serverUrl"": ""http://seq.avalon:5341"",
                                    ""restrictedToMinimumLevel"": ""Verbose""
                                }
                            }
                        ]
                    }
                }
            ]
        }
    }";
}

