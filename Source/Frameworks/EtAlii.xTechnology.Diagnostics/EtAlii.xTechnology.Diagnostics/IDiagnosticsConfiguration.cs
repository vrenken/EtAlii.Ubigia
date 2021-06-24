// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Diagnostics
{
    using System;

    public interface IDiagnosticsConfiguration
    {
        bool EnableProfiling { get; set; }
        bool EnableLogging { get; set; }
        bool EnableDebugging { get; set; }
        
        Func<IProfilerFactory> CreateProfilerFactory { get; set; }
        Func<IProfilerFactory, IProfiler> CreateProfiler { get; set; }
    }
}