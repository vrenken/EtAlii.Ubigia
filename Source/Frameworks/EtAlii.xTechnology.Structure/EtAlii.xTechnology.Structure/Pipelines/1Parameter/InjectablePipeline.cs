namespace EtAlii.xTechnology.Structure.Pipelines
{
    using System;

    public class InjectablePipeline<TPipelineIn> :
        Pipeline<TPipelineIn>,
        IInjectablePipeline<TPipelineIn>
    {
        // ReSharper disable once NotAccessedField.Local
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
        // ReSharper disable once NotAccessedField.Local
        private readonly IServiceProvider _serviceProvider;

        public InjectablePipeline(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}