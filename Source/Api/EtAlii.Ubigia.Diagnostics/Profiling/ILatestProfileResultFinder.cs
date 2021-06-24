// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Diagnostics.Profiling
{
    using System.Collections.Generic;

    public interface ILatestProfileResultFinder
    {
        ProfilingResult Find(IEnumerable<ProfilingResult> results, ProfilingLayer currentLayer);
    }
}
