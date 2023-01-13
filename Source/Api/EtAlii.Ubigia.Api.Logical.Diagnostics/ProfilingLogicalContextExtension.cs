// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics;

using EtAlii.Ubigia.Diagnostics.Profiling;
using EtAlii.xTechnology.Diagnostics;
using EtAlii.xTechnology.MicroContainer;
using Microsoft.Extensions.Configuration;
using IProfiler = EtAlii.Ubigia.Diagnostics.Profiling.IProfiler;

public sealed class ProfilingLogicalContextExtension : IExtension
{
    private readonly IConfigurationRoot _configurationRoot;

    public ProfilingLogicalContextExtension(IConfigurationRoot configurationRoot)
    {
        _configurationRoot = configurationRoot;
    }


    public void Initialize(IRegisterOnlyContainer container)
    {
        var options = _configurationRoot
            .GetSection("Api:Logical:Diagnostics")
            .Get<DiagnosticsOptions>();

        if (options.InjectProfiling)
        {
            container.RegisterDecorator<ILogicalContext, ProfilingLogicalContext>();

            container.RegisterDecorator<ILogicalRootSet, ProfilingLogicalRootSet>();
            container.RegisterDecorator<IPropertiesManager, ProfilingPropertiesManager>();
            container.RegisterDecorator<ILogicalNodeSet, ProfilingLogicalNodeSet>();
            container.RegisterDecorator<IContentManager, ProfilingContentManager>();

            container.RegisterDecorator<IGraphPathTraverser, ProfilingGraphPathTraverser>();

            container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Logical.Context));
        }
    }
}
