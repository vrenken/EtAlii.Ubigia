// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Pipelines
{
    using System;

    public class InjectablePipeline<TPipelineIn> :
        Pipeline<TPipelineIn>,
        IInjectablePipeline<TPipelineIn>
    {
        // ReSharper disable once NotAccessedField.Local
#pragma warning disable S4487        
        private readonly IServiceProvider _serviceProvider;
#pragma warning restore S4487        

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
#pragma warning disable S4487        
        private readonly IServiceProvider _serviceProvider;
#pragma warning restore S4487        

        public InjectablePipeline(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}