// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Pipelines
{
    using System;

    public class DefaultOperationRegistration<TPipelineIn, TPipelineOut, TIn, TOut> : 
        IOperationRegistration<TPipelineIn, TPipelineOut, TIn, TOut>,
        IPipelineProvider<TPipelineIn, TPipelineOut>,
        IOperationChain<TPipelineOut, TIn, TOut>
    {
        private readonly IPipeline<TPipelineIn, TPipelineOut> _pipeline;
        private readonly Func<TIn, TOut> _operation;
        private Func<TOut, TPipelineOut> _nextOperation;

        public DefaultOperationRegistration(
            IPipeline<TPipelineIn, TPipelineOut> pipeline, 
            Func<TIn, TOut> operation)
        {
            _pipeline = pipeline;
            _operation = operation;
        }

        IPipeline<TPipelineIn, TPipelineOut> IPipelineProvider<TPipelineIn, TPipelineOut>.GetPipeline()
        {
            return _pipeline;
        }


        void IOperationChain<TPipelineOut, TIn, TOut>.Register(Func<TOut, TPipelineOut> nextOperation)
        {
            if (_nextOperation != null)
            {
                var message = "This pipeline has already been configured.";
                throw new InvalidOperationException(message);
            }
            _nextOperation = nextOperation;
        }

        TPipelineOut IOperationChain<TPipelineOut, TIn, TOut>.Process(TIn input)
        {
            if (_nextOperation == null)
            {
                var message = "This operation has not yet been configured.";
                throw new InvalidOperationException(message);
            }
            var output = _operation(input);
            return _nextOperation(output);
        }
    }

    public class DefaultOperationRegistration<TPipelineIn, TIn, TOut> : 
        IOperationRegistration<TPipelineIn, TIn, TOut>,
        IPipelineProvider<TPipelineIn>,
        IOperationChain<TIn, TOut>
    {
        private readonly IPipeline<TPipelineIn> _pipeline;
        private readonly Func<TIn, TOut> _operation;
        private Action<TOut> _nextOperation;

        public DefaultOperationRegistration(
            IPipeline<TPipelineIn> pipeline, 
            Func<TIn, TOut> operation)
        {
            _pipeline = pipeline;
            _operation = operation;
        }

        IPipeline<TPipelineIn> IPipelineProvider<TPipelineIn>.GetPipeline()
        {
            return _pipeline;
        }

        void IOperationChain<TIn, TOut>.Register(Action<TOut> nextOperation)
        {
            if (_nextOperation != null)
            {
                var message = "This operation has already been configured.";
                throw new InvalidOperationException(message);
            }
            _nextOperation = nextOperation;
        }

        void IOperationChain<TIn, TOut>.Process(TIn input)
        {
            if (_nextOperation == null)
            {
                var message = "This operation has not yet been configured.";
                throw new InvalidOperationException(message);
            }
            var output = _operation(input);
            _nextOperation(output);
        }
    }
}