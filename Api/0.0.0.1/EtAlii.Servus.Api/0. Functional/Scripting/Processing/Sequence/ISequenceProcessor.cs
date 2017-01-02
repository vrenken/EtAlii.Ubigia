namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal interface ISequenceProcessor
    {
        Task Process(
            Sequence sequence, 
            IObserver<object> output);
    }
}