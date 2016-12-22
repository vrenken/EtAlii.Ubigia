namespace EtAlii.xTechnology.Structure.Pipelines2
{
    public interface IInjectablePipeline<TPipelineIn> : IPipeline<TPipelineIn>
    {
    }


    public interface IInjectablePipeline<TPipelineIn, TPipelineOut> : IPipeline<TPipelineIn, TPipelineOut>
    {
    }
}