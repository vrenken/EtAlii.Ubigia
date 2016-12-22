namespace EtAlii.xTechnology.Structure.Pipelines2
{
    using System;

    public class InjectablePipeline<TPipelineIn> :
        Pipeline<TPipelineIn>,
        IInjectablePipeline<TPipelineIn>
    {
        private readonly IServiceProvider _serviceProvider;

        public InjectablePipeline(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }


    public class InjectablePipeline<TPipelineIn, TPipelineOut> :
        Pipeline<TPipelineIn, TPipelineOut>,
        IInjectablePipeline<TPipelineIn, TPipelineOut>
    {
        private readonly IServiceProvider _serviceProvider;

        public InjectablePipeline(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}