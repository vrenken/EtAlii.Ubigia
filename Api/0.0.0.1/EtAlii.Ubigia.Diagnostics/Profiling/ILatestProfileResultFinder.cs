namespace EtAlii.Ubigia.Diagnostics.Profiling
{
    using System.Collections.Generic;

    public interface ILatestProfileResultFinder
    {
        ProfilingResult Find(IEnumerable<ProfilingResult> results, ProfilingLayer currentLayer);
    }
}