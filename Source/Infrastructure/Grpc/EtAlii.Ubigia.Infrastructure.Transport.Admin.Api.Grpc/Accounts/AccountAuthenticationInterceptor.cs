// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using global::Grpc.Core;
    using global::Grpc.Core.Interceptors;

    public class AccountAuthenticationInterceptor : Interceptor, IAccountAuthenticationInterceptor
    {
       private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;

        public AccountAuthenticationInterceptor(ISimpleAuthenticationTokenVerifier authenticationTokenVerifier)
        {
            _authenticationTokenVerifier = authenticationTokenVerifier;
        }

        private void EnsureUserIsAuthenticated(Metadata headers)
        {
            var authenticationTokenHeader = headers.SingleOrDefault(h => h.Key == GrpcHeader.AuthenticationTokenHeaderKey);

            if (authenticationTokenHeader == null)
            {
                throw new InvalidOperationException("Unable to authenticate: no authentication token header.");
            }

            var authenticationToken = authenticationTokenHeader.Value;
            _authenticationTokenVerifier.Verify(authenticationToken, Role.Admin, Role.System);

            // The EnsureUserIsAuthenticated method should also check the space ID.
            // More information can be found in the Github issue below:
            // https://github.com/vrenken/EtAlii.Ubigia/issues/78
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            EnsureUserIsAuthenticated(context.RequestHeaders);
            return base.UnaryServerHandler(request, context, continuation);
        }

        public override TResponse BlockingUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context,
            BlockingUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            EnsureUserIsAuthenticated(context.Options.Headers);
            return base.BlockingUnaryCall(request, context, continuation);
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            EnsureUserIsAuthenticated(context.Options.Headers);
            return base.AsyncUnaryCall(request, context, continuation);
        }

        public override AsyncClientStreamingCall<TRequest, TResponse> AsyncClientStreamingCall<TRequest, TResponse>(
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncClientStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            EnsureUserIsAuthenticated(context.Options.Headers);
            return base.AsyncClientStreamingCall(context, continuation);
        }

        public override AsyncDuplexStreamingCall<TRequest, TResponse> AsyncDuplexStreamingCall<TRequest, TResponse>(
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncDuplexStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            EnsureUserIsAuthenticated(context.Options.Headers);
            return base.AsyncDuplexStreamingCall(context, continuation);
        }

        public override AsyncServerStreamingCall<TResponse> AsyncServerStreamingCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncServerStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            EnsureUserIsAuthenticated(context.Options.Headers);
            return base.AsyncServerStreamingCall(request, context, continuation);
        }

        public override Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(
            IAsyncStreamReader<TRequest> requestStream,
            ServerCallContext context,
            ClientStreamingServerMethod<TRequest, TResponse> continuation)
        {
            EnsureUserIsAuthenticated(context.RequestHeaders);
            return base.ClientStreamingServerHandler(requestStream, context, continuation);
        }

        public override Task DuplexStreamingServerHandler<TRequest, TResponse>(
            IAsyncStreamReader<TRequest> requestStream,
            IServerStreamWriter<TResponse> responseStream,
            ServerCallContext context,
            DuplexStreamingServerMethod<TRequest, TResponse> continuation)
        {
            EnsureUserIsAuthenticated(context.RequestHeaders);
            return base.DuplexStreamingServerHandler(requestStream, responseStream, context, continuation);
        }

        public override Task ServerStreamingServerHandler<TRequest, TResponse>(
            TRequest request,
            IServerStreamWriter<TResponse> responseStream,
            ServerCallContext context,
            ServerStreamingServerMethod<TRequest, TResponse> continuation)
        {
            EnsureUserIsAuthenticated(context.RequestHeaders);
            return base.ServerStreamingServerHandler(request, responseStream, context, continuation);
        }

    }
}
