namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    using EtAlii.Servus.Api.Logical;

    public interface IProfilingGraphPathTraverser : IGraphPathTraverser
    {
        IProfiler Profiler { get; }
    }
}