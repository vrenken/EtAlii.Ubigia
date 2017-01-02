namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    public interface IProfilingContext
    {
        IProfiler Profiler { get; }
    }
}