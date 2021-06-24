// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Diagnostics
{
    using System;

    public partial class DiagnosticsConfiguration : IDiagnosticsConfiguration
    {
        public bool EnableProfiling { get; set; }
        public bool EnableLogging { get; set; }
        public bool EnableDebugging { get; set; }
        
        public Func<IProfilerFactory> CreateProfilerFactory { get; set; }
        public Func<IProfilerFactory, IProfiler> CreateProfiler { get; set; }

        public static readonly IDiagnosticsConfiguration Default = new DiagnosticsConfiguration
        {
            EnableProfiling = false,
            EnableLogging = true,
            EnableDebugging = true,
            CreateProfilerFactory = () => new DisabledProfilerFactory(),
            CreateProfiler = CreateProfilerInstance,//factory => factory.Create("EtAlii", "Default"),
        };
     
        private static IProfiler CreateProfilerInstance(IProfilerFactory factory)
        {
            return factory.Create("EtAlii", "EtAlii.Ubigia");
        }
    }
}