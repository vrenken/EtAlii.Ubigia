namespace EtAlii.Ubigia.Diagnostics.Profiling
{
    public interface IProfilingContext
    {
        IProfiler Profiler { get; }
    }
}