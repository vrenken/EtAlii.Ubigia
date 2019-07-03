namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    internal interface IQueryFragmentProcessor : IFragmentProcessor
    {
        Task Process(QueryFragment fragment, QueryExecutionScope executionScope, FragmentContext fragmentContext, IObserver<Structure> output);
    }
}
