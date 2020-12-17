namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using global::Grpc.Core.Interceptors;
    using global::EtAlii.xTechnology.Threading;
    using global::Grpc.Core;

	public class CorrelationCallInterceptor : Interceptor
    {
        private readonly IContextCorrelator _contextCorrelator;

        public CorrelationCallInterceptor(IContextCorrelator contextCorrelator)
        {
            _contextCorrelator = contextCorrelator;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            AddHeadersWhenNeeded(context.Options.Headers);
            return base.AsyncUnaryCall(request, context, continuation);
        }

        public override TResponse BlockingUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context,
            BlockingUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            AddHeadersWhenNeeded(context.Options.Headers);
            return base.BlockingUnaryCall(request, context, continuation);
        }

        public override AsyncClientStreamingCall<TRequest, TResponse> AsyncClientStreamingCall<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context,
            AsyncClientStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            AddHeadersWhenNeeded(context.Options.Headers);
            return base.AsyncClientStreamingCall(context, continuation);
        }

        public override AsyncDuplexStreamingCall<TRequest, TResponse> AsyncDuplexStreamingCall<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context,
            AsyncDuplexStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            AddHeadersWhenNeeded(context.Options.Headers);
            return base.AsyncDuplexStreamingCall(context, continuation);
        }

        private void AddHeadersWhenNeeded(Metadata metadata)
        {
            if (_contextCorrelator.TryGetValue(Correlation.ScriptId, out var correlationId))
            {
                metadata.Add(Correlation.ScriptId, correlationId);
            }
        }
    }
}
