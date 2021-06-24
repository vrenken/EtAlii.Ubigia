// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Pipelines
{
    public interface IInjectablePipeline<in TPipelineIn> : IPipeline<TPipelineIn>
    {
    }


    public interface IInjectablePipeline<in TPipelineIn, out TPipelineOut> : IPipeline<TPipelineIn, TPipelineOut>
    {
    }
}