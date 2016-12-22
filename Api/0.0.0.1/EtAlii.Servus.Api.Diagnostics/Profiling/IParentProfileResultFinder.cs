namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    public interface IParentProfileResultFinder
    {
        ProfilingResult Find(IProfiler profiler);
    }
}