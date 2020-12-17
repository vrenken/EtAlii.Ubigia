namespace EtAlii.Ubigia.Infrastructure.Transport.Grpc
{
    using System;
    using System.Threading.Tasks;
    using Serilog;
    using global::Grpc.Core.Interceptors;
    using global::EtAlii.xTechnology.Threading;
    using global::Grpc.Core;
    using EtAlii.xTechnology.Diagnostics;

	public class CorrelationServiceInterceptor : Interceptor
    {
        private readonly IContextCorrelator _contextCorrelator;
        private readonly ILogger _logger = Log.ForContext<CorrelationServiceInterceptor>();

        public CorrelationServiceInterceptor(IContextCorrelator contextCorrelator)
        {
            _contextCorrelator = contextCorrelator;
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request, ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            AddCorrelationIdsFromHeaders(context.RequestHeaders);
            return base.UnaryServerHandler(request, context, continuation);
        }

        public override Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(
            IAsyncStreamReader<TRequest> requestStream, ServerCallContext context,
            ClientStreamingServerMethod<TRequest, TResponse> continuation)
        {
            AddCorrelationIdsFromHeaders(context.RequestHeaders);
            return base.ClientStreamingServerHandler(requestStream, context, continuation);
        }

        public override Task DuplexStreamingServerHandler<TRequest, TResponse>(
            IAsyncStreamReader<TRequest> requestStream,
            IServerStreamWriter<TResponse> responseStream, ServerCallContext context,
            DuplexStreamingServerMethod<TRequest, TResponse> continuation)
        {
            AddCorrelationIdsFromHeaders(context.RequestHeaders);
            return base.DuplexStreamingServerHandler(requestStream, responseStream, context, continuation);
        }

        public override Task ServerStreamingServerHandler<TRequest, TResponse>(
            TRequest request, IServerStreamWriter<TResponse> responseStream,
            ServerCallContext context, ServerStreamingServerMethod<TRequest, TResponse> continuation)
        {
            AddCorrelationIdsFromHeaders(context.RequestHeaders);
            return base.ServerStreamingServerHandler(request, responseStream, context, continuation);
        }

        private void AddCorrelationIdsFromHeaders(Metadata metadata)
        {
            foreach (var header in metadata)
            {
                var key = header.Key;
                var value = header.Value;

                _logger.Debug("Interpreting header {header}/{value}", key, value);
                foreach (var correlationId in Correlation.AllIds)
                {
                    if (string.Equals(correlationId, key, StringComparison.OrdinalIgnoreCase))
                    {
                        _contextCorrelator.BeginLoggingCorrelationScope(correlationId, value);
                        break;
                    }
                }
            }
        }
    }
}
