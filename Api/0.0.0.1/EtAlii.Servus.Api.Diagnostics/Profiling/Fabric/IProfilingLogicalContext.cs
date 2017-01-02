namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    using EtAlii.Servus.Api.Logical;

    public interface IProfilingLogicalContext : ILogicalContext
    {
        IProfiler Profiler { get; }
    }
}