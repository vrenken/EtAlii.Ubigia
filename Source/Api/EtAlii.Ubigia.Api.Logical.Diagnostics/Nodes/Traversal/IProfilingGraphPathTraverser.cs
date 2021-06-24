// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.Ubigia.Diagnostics.Profiling;

    public interface IProfilingGraphPathTraverser : IGraphPathTraverser
    {
        IProfiler Profiler { get; }
    }
}
