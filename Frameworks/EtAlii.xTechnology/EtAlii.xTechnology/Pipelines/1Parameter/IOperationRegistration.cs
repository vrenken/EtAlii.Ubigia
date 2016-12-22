namespace EtAlii.xTechnology.Structure.Pipelines2
{
    public interface IOperationRegistration<in TPipelineIn, out TPipelineOut, in TIn, out TOut>
    {
    }

    public interface IOperationRegistration<in TPipelineIn, in TIn, out TOut>
    {
    }
}