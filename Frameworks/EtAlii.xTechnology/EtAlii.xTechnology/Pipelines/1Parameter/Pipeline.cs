namespace EtAlii.xTechnology.Structure.Pipelines2
{
    using System;

    public class Pipeline<TPipelineIn> : 
        IPipeline<TPipelineIn>, 
        IOperationRegistrationFactoryProvider<TPipelineIn>,
        IOperationChain<TPipelineIn, TPipelineIn>
    {
        private Action<TPipelineIn> _nextOperation;

        IOperationRegistrationFactory<TPipelineIn, TIn, TOut> IOperationRegistrationFactoryProvider<TPipelineIn>.GetFactory<TIn, TOut>()
        {
            return new DefaultOperationRegistrationFactory<TPipelineIn, TIn, TOut>(this);
        }

        void IOperationChain<TPipelineIn, TPipelineIn>.Register(Action<TPipelineIn> nextOperation)
        {
            if (_nextOperation != null)
            {
                var message = String.Format("This pipeline has already been configured.");
                throw new InvalidOperationException(message);
            }
            _nextOperation = nextOperation;
        }

        void IOperationChain<TPipelineIn, TPipelineIn>.Process(TPipelineIn input)
        {
            if (_nextOperation == null)
            {
                var message = String.Format("This pipeline has not yet been configured.");
                throw new InvalidOperationException(message);
            }
            _nextOperation(input);
        }

        public void Process(TPipelineIn input)
        {
            var operationChain = (IOperationChain<TPipelineIn, TPipelineIn>)this;
            operationChain.Process(input);
        }
    }

    public class Pipeline<TPipelineIn, TPipelineOut> : 
        IPipeline<TPipelineIn, TPipelineOut>, 
        IOperationRegistrationFactoryProvider<TPipelineIn, TPipelineOut>,
        IOperationChain<TPipelineOut, TPipelineIn, TPipelineIn>
    {
        private Func<TPipelineIn, TPipelineOut> _nextOperation;

        IOperationRegistrationFactory<TPipelineIn, TPipelineOut, TIn, TOut> IOperationRegistrationFactoryProvider<TPipelineIn, TPipelineOut>.GetFactory<TIn, TOut>()
        {
            return new DefaultOperationRegistrationFactory<TPipelineIn, TPipelineOut, TIn, TOut>(this);
        }

        void IOperationChain<TPipelineOut, TPipelineIn, TPipelineIn>.Register(Func<TPipelineIn, TPipelineOut> nextOperation)
        {
            if (_nextOperation != null)
            {
                var message = String.Format("This pipeline has already been configured.");
                throw new InvalidOperationException(message);
            }
            _nextOperation = nextOperation;
        }


        TPipelineOut IOperationChain<TPipelineOut, TPipelineIn, TPipelineIn>.Process(TPipelineIn input)
        {
            if (_nextOperation == null)
            {
                var message = String.Format("This pipeline has not yet been configured.");
                throw new InvalidOperationException(message);
            }
            return _nextOperation(input);
        }

        public TPipelineOut Process(TPipelineIn input)
        {
            var operationChain = (IOperationChain<TPipelineOut, TPipelineIn, TPipelineIn>)this;
            return operationChain.Process(input);
        }
    }
}