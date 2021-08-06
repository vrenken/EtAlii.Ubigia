// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;
    using IProfiler = EtAlii.Ubigia.Diagnostics.Profiling.IProfiler;

    public class ProfilingGraphContextExtension : IGraphContextExtension
    {
        public void Initialize(Container container)
        {
            var configurationRoot = container.GetInstance<IConfigurationRoot>();
            var options = configurationRoot
                .GetSection("Api:Functional:Diagnostics")
                .Get<DiagnosticsOptions>();

            if (options.InjectProfiling)
            {
                container.RegisterDecorator(typeof(IGraphContext), typeof(ProfilingGraphContext));

                //container.RegisterDecorator(typeof(IQueryProcessorFactory), typeof(ProfilingQueryProcessorFactory))
                //container.RegisterDecorator(typeof(IQueryParserFactory), typeof(ProfilingQueryParserFactory))

                container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Functional.GraphContext));
            }
        }
    }
}
