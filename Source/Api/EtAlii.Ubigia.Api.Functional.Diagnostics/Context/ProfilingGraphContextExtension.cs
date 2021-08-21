// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;
    using IProfiler = EtAlii.Ubigia.Diagnostics.Profiling.IProfiler;

    public class ProfilingGraphContextExtension : IExtension
    {
        private readonly IConfigurationRoot _configurationRoot;

        public ProfilingGraphContextExtension(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        public void Initialize(IRegisterOnlyContainer container)
        {
            var options = _configurationRoot
                .GetSection("Api:Functional:Diagnostics")
                .Get<DiagnosticsOptions>();

            if (options?.InjectProfiling ?? false)
            {
                container.RegisterDecorator<IGraphContext, ProfilingGraphContext>();

                //container.RegisterDecorator(typeof(IQueryProcessorFactory), typeof(ProfilingQueryProcessorFactory))
                //container.RegisterDecorator(typeof(IQueryParserFactory), typeof(ProfilingQueryParserFactory))

                container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Functional.GraphContext));
            }
        }
    }
}
