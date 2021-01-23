namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;

    internal interface IResultConverter
    {
        Task Convert(object input, ExecutionScope scope, IObserver<object> output);

    }
}
