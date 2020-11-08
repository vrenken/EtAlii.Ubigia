namespace EtAlii.xTechnology.Structure.Pipelines
{
    public static partial class OperationRegistrationPipelineExtensions
    {
        public static IPipeline<TPipelineIn, TPipelineOut> EndWith<TPipelineIn, TPipelineOut, TIn, TOut>(
            this IOperationRegistration<TPipelineIn, TPipelineOut, TIn, TOut> registration, IPipeline<TOut, TPipelineOut> pipeline2)
        {
            var pipelineProvider = (IPipelineProvider<TPipelineIn, TPipelineOut>)registration;
            var pipeline = pipelineProvider.GetPipeline();
            var factoryProvider = (IOperationRegistrationFactoryProvider<TPipelineIn, TPipelineOut>)pipeline;
            var factory = factoryProvider.GetFactory<TOut, TPipelineOut>();
            var newRegistration = factory.Create(pipeline2.Process);

            var operationChain = (IOperationChain<TPipelineOut, TIn, TOut>)registration;
            var nextOperation = (IOperationChain<TPipelineOut, TOut, TPipelineOut>)newRegistration;
            operationChain.Register(nextOperation.Process);
            nextOperation.Register(v => v);

            return pipeline;
        }

        public static IPipeline<TPipelineIn> EndWith<TPipelineIn, TIn, TOut>(
        this IOperationRegistration<TPipelineIn, TIn, TOut> registration, IPipeline<TOut> pipeline2)
        {
            var pipelineProvider = (IPipelineProvider<TPipelineIn>)registration;
            var pipeline = pipelineProvider.GetPipeline();
            var factoryProvider = (IOperationRegistrationFactoryProvider<TPipelineIn>)pipeline;
            var factory = factoryProvider.GetFactory<TOut, TOut>();
            var newRegistration = factory.Create(input =>
            {
                pipeline2.Process(input);
                return default(TOut);
            });

            var operationChain = (IOperationChain<TIn, TOut>)registration;
            var nextOperation = (IOperationChain<TOut, TOut>)newRegistration;
            operationChain.Register(nextOperation.Process);
            nextOperation.Register(v => { });

            return pipeline;
        }
    }
}