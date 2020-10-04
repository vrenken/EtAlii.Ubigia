namespace EtAlii.Ubigia.Diagnostics.Profiling
{
    public interface IParentProfileResultFinder
    {
        ProfilingResult Find(IProfiler profiler);
    }
}