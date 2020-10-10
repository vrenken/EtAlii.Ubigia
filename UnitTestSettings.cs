using Xunit;

// Settings in this file are shared across most Unit test projects. 
//[assembly: CollectionBehavior(CollectionBehavior.CollectionPerClass, DisableTestParallelization = false)]

#if UBIGIA_RUNNING_ON_BUILD_AGENT
[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly, DisableTestParallelization = false)]
#else
[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly, DisableTestParallelization = true)]
#endif 
 