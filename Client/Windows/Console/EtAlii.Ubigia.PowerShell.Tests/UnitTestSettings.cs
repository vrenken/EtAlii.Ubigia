using Xunit;

// For now the PowerShell API requires a bit more delicate testing. 
[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly, DisableTestParallelization = true)]
