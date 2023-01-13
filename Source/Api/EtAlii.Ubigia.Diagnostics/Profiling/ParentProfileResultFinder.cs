// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Diagnostics.Profiling;

using System.Linq;

public class ParentProfileResultFinder : IParentProfileResultFinder
{
    private readonly ILatestProfileResultFinder _latestProfileResultFinder;
    public ParentProfileResultFinder()
    {
        _latestProfileResultFinder = new LatestProfileResultFinder();
    }

    public ProfilingResult Find(IProfiler profiler)
    {
        // First lets check on the local result stack.
        if (profiler.ResultStack.Any())
        {
            return profiler.ResultStack.Peek();
        }

        // Nope, no result found. Then lets look if we can find a parent profiling result.
        if (profiler.Parent != null)
        {
            return Find(profiler.Parent);
        }


        // Also no success. Then we need to find a previous profiler instead.
        var previous = profiler.Previous;
        while (previous != null)
        {
            var result = _latestProfileResultFinder.Find(previous.ResultStack, profiler.Aspect.Layer);
            if (result != null)
            {
                return result;
            }

            previous = previous.Previous;
        }
        return null;
    }
}
