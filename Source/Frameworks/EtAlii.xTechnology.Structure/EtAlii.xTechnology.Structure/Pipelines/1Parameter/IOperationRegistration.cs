namespace EtAlii.xTechnology.Structure.Pipelines
{
    // ReSharper disable UnusedTypeParameter
    public interface IOperationRegistration<in TPipelineIn, out TPipelineOut, in TIn, out TOut>
    {
    }

    public interface IOperationRegistration<in TPipelineIn, in TIn, out TOut>
    {
    }
    // ReSharper restore UnusedTypeParameter
}