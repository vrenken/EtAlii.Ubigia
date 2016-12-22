namespace EtAlii.xTechnology.Structure.Pipelines2
{
    public interface IPipeline<in TPipelineIn>
    {
        void Process(TPipelineIn input);
    }

    public interface IPipeline<in TPipelineIn, out TPipelineOut>
    {
        TPipelineOut Process(TPipelineIn input);
    }
}