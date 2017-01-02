namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal interface IExecutionPlanStep<in TIn, in TOut>
    {
        void Initialize(IObservable<TIn> input, IObservable<TOut> output);
    }
}