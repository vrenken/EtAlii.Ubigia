namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    public interface IProfilingContext
    {
        IProfiler Profiler { get; }
    }
}