namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.Ubigia.Diagnostics.Profiling;

    public interface IProfilingGraphPathTraverser : IGraphPathTraverser
    {
        IProfiler Profiler { get; }
    }
}