namespace EtAlii.xTechnology.Structure.Pipelines2
{
    using System;

    public interface IOperationChain<TFinalOut, in TIn, out TOut>
    {
        void Register(Func<TOut, TFinalOut> nextOperation);
        TFinalOut Process(TIn input);
    }

    public interface IOperationChain<in TIn, out TOut>
    {
        void Register(Action<TOut> nextOperation);
        void Process(TIn input);
    }
}