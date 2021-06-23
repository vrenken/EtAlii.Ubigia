// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Diagnostics.Profiling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LatestProfileResultFinder : ILatestProfileResultFinder
    {
        public ProfilingResult Find(IEnumerable<ProfilingResult> results, ProfilingLayer currentLayer)
        {
            var result = results.LastOrDefault(r => r.Layer != currentLayer);
            if (result != null)
            {
                var subResult = Find(result.Children, currentLayer);
                if (subResult != null && subResult.Stopped == DateTime.MinValue)
                {
                    result = subResult;
                }
            }
            return result;
        }

    }
}