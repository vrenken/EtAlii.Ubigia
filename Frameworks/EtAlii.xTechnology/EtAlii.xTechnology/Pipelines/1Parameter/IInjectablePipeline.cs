namespace EtAlii.xTechnology.Structure.Pipelines2
{
    public interface IInjectablePipeline<in TPipelineIn> : IPipeline<TPipelineIn>
    {
    }


    public interface IInjectablePipeline<in TPipelineIn, out TPipelineOut> : IPipeline<TPipelineIn, TPipelineOut>
    {
    }
}