// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;
    using IProfiler = EtAlii.Ubigia.Diagnostics.Profiling.IProfiler;

    public class ProfilingLogicalContextExtension : ILogicalContextExtension
    {
        public void Initialize(Container container)
        {
            var configurationRoot = container.GetInstance<IConfiguration>();
            var options = configurationRoot
                .GetSection("Api:Logical:Diagnostics")
                .Get<DiagnosticsOptions>();

            if (options.InjectProfiling)
            {
                container.RegisterDecorator(typeof(ILogicalContext), typeof(ProfilingLogicalContext));

                container.RegisterDecorator(typeof(ILogicalRootSet), typeof(ProfilingLogicalRootSet));
                container.RegisterDecorator(typeof(IPropertiesManager), typeof(ProfilingPropertiesManager));
                container.RegisterDecorator(typeof(ILogicalNodeSet), typeof(ProfilingLogicalNodeSet));
                container.RegisterDecorator(typeof(IContentManager), typeof(ProfilingContentManager));

                container.RegisterDecorator(typeof(IGraphPathTraverserFactory), typeof(ProfilingGraphPathTraverserFactory));

                container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Logical.Context));
            }
        }
    }
}
