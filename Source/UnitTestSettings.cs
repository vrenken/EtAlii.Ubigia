// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

using Xunit;
using System.Runtime.CompilerServices;
using EtAlii.xTechnology.Diagnostics;

// We want to run the unit tests in sequence on the build agent.
#if UBIGIA_IS_RUNNING_ON_BUILD_AGENT
[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly, DisableTestParallelization = false)]
#else
[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly, DisableTestParallelization = false)] // CollectionPerAssembly
#endif

internal static class UnitTestConstants
{
    public const int NetworkPortRangeStart = 20000;
}

internal static class LoggingInitializer
{
    [ModuleInitializer]
    internal static void Initialize()
    {
        DiagnosticsConfiguration.Initialize(typeof(LoggingInitializer).Assembly);
    }
}
