﻿using Xunit;

// We want to run the unit tests in sequence on the build agent. 
#if UBIGIA_IS_RUNNING_ON_BUILD_AGENT
[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly, DisableTestParallelization = true)]
#else
[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly, DisableTestParallelization = false)] // CollectionPerClass
#endif

internal static class UnitTestConstants
{
    public const int NetworkPortRangeStart = 20000; 
}  