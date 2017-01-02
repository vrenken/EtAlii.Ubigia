namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    public interface IParentProfileResultFinder
    {
        ProfilingResult Find(IProfiler profiler);
    }
}