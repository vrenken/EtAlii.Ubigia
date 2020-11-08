namespace EtAlii.xTechnology.Structure.Pipelines
{
    public interface IInjectablePipeline<in TPipelineIn> : IPipeline<TPipelineIn>
    {
    }


    public interface IInjectablePipeline<in TPipelineIn, out TPipelineOut> : IPipeline<TPipelineIn, TPipelineOut>
    {
    }
}