namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal class QueryFragmentProcessor : IQueryFragmentProcessor
    {
        public Task Process(Fragment fragment, QueryExecutionScope scope, IObserver<object> output)
        {
            throw new NotImplementedException();
        }
    }
}