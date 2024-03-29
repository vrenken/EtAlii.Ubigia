// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics;

using System;
using System.Threading.Tasks;
using EtAlii.Ubigia.Diagnostics.Profiling;

public class ProfilingLogicalContext : IProfilingLogicalContext
{
    private readonly ILogicalContext _decoree;

    public IProfiler Profiler { get; }

    public LogicalOptions Options => _decoree.Options;
    public ILogicalNodeSet Nodes => _decoree.Nodes;
    public ILogicalRootSet Roots => _decoree.Roots;
    public IContentManager Content => _decoree.Content;
    public IPropertiesManager Properties => _decoree.Properties;

    public ProfilingLogicalContext(ILogicalContext decoree, IProfiler profiler)
    {
        _decoree = decoree;
        Profiler = profiler;
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        await _decoree
            .DisposeAsync()
            .ConfigureAwait(false);
    }
}
