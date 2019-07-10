namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;

    internal interface IQueryFragmentProcessor : IFragmentProcessor
    {
        Task Process(
            QueryFragment fragment, 
            QueryExecutionScope executionScope, 
            FragmentMetadata fragmentMetadata, 
            IObserver<Structure> output);
    }
}
