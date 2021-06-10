using Xunit;

// We want to run the unit tests in sequence on the build agent.
#if UBIGIA_IS_RUNNING_ON_BUILD_AGENT
[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly, DisableTestParallelization = false)]
#else
[assembly: CollectionBehavior(CollectionBehavior.CollectionPerClass, DisableTestParallelization = false)] // CollectionPerAssembly
#endif

internal static class UnitTestConstants
{
    public const int NetworkPortRangeStart = 20000;
}
