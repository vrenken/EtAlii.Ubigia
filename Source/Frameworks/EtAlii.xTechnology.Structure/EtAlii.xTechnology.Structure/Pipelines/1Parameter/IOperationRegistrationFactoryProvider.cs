namespace EtAlii.xTechnology.Structure.Pipelines
{
    public interface IOperationRegistrationFactoryProvider<in TPipelineIn>
    {
        IOperationRegistrationFactory<TPipelineIn, TIn, TOut> GetFactory<TIn, TOut>();
    }

    public interface IOperationRegistrationFactoryProvider<in TPipelineIn, out TPipelineOut>
    {
        IOperationRegistrationFactory<TPipelineIn, TPipelineOut, TIn, TOut> GetFactory<TIn, TOut>();
    }
}