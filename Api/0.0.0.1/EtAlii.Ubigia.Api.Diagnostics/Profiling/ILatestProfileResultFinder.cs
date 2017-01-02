namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using System.Collections.Generic;

    public interface ILatestProfileResultFinder
    {
        ProfilingResult Find(IEnumerable<ProfilingResult> results, ProfilingLayer currentLayer);
    }
}