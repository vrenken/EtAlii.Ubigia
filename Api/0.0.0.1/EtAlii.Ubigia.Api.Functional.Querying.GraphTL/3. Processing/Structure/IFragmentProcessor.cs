namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal interface IFragmentProcessor
    {
        Task Process(Fragment fragment, QueryExecutionScope scope, IObserver<object> output);
    }
}
