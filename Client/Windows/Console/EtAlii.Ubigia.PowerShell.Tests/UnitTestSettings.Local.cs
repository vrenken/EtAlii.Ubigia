using Xunit;

// TODO: PowerShell doesn't like when tests are run in parallel.
[assembly: CollectionBehavior(collectionBehavior: CollectionBehavior.CollectionPerAssembly, DisableTestParallelization = false)]