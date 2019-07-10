namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;

    internal interface IMutationFragmentProcessor : IFragmentProcessor
    {
        Task Process(
            MutationFragmentExecutionPlan plan, 
            QueryExecutionScope executionScope, 
            FragmentMetadata fragmentMetadata, 
            IObserver<Structure> output);
    }
}
