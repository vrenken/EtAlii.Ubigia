namespace EtAlii.xTechnology.Structure.Pipelines2
{
    using System;

    public interface IOperationRegistrationFactory<in TPipelineIn, out TPipelineOut, TIn, TOut>
    {
        IOperationRegistration<TPipelineIn, TPipelineOut, TIn, TOut> Create(Func<TIn, TOut> operation);
    }

    public interface IOperationRegistrationFactory<in TPipelineIn, TIn, TOut>
    {
        IOperationRegistration<TPipelineIn, TIn, TOut> Create(Func<TIn, TOut> operation);
    }
}