namespace EtAlii.xTechnology.Structure.Pipelines2
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