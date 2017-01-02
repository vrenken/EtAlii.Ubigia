namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using EtAlii.Ubigia.Api.Logical;

    public interface IProfilingGraphPathTraverser : IGraphPathTraverser
    {
        IProfiler Profiler { get; }
    }
}