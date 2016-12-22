namespace EtAlii.xTechnology.Structure.Pipelines2
{
    public interface IPipelineProvider<in TPipelineIn>
    {
        IPipeline<TPipelineIn> GetPipeline();
    }

    public interface IPipelineProvider<in TPipelineIn, out TPipelineOut>
    {
        IPipeline<TPipelineIn, TPipelineOut> GetPipeline();
    }
}