// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Grpc;

using System;
using System.Threading.Tasks;
using global::Grpc.Core.Interceptors;
using global::EtAlii.xTechnology.Threading;
using global::Grpc.Core;
using EtAlii.xTechnology.Diagnostics;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// The task of the CorrelationServiceInterceptor is to fetch any correlation Id
/// out of the Grpc metadata and re-apply it as a correlation scope.
/// This way the correlation Id's cascade from the client into the server.
/// </summary>
public class CorrelationServiceInterceptor : Interceptor
{
    private readonly IContextCorrelator _contextCorrelator;

    public CorrelationServiceInterceptor(IContextCorrelator contextCorrelator)
    {
        _contextCorrelator = contextCorrelator;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request, ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        using var correlations = BeginCorrelation(context.RequestHeaders);
        return await base.UnaryServerHandler(request, context, continuation).ConfigureAwait(false);
    }

    public override async Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(
        IAsyncStreamReader<TRequest> requestStream, ServerCallContext context,
        ClientStreamingServerMethod<TRequest, TResponse> continuation)
    {
        using var correlations = BeginCorrelation(context.RequestHeaders);
        return await base.ClientStreamingServerHandler(requestStream, context, continuation).ConfigureAwait(false);
    }

    public override async Task DuplexStreamingServerHandler<TRequest, TResponse>(
        IAsyncStreamReader<TRequest> requestStream,
        IServerStreamWriter<TResponse> responseStream, ServerCallContext context,
        DuplexStreamingServerMethod<TRequest, TResponse> continuation)
    {
        using var correlations = BeginCorrelation(context.RequestHeaders);
        await base.DuplexStreamingServerHandler(requestStream, responseStream, context, continuation).ConfigureAwait(false);
    }

    public override async Task ServerStreamingServerHandler<TRequest, TResponse>(
        TRequest request, IServerStreamWriter<TResponse> responseStream,
        ServerCallContext context, ServerStreamingServerMethod<TRequest, TResponse> continuation)
    {
        using var correlations = BeginCorrelation(context.RequestHeaders);
        await base.ServerStreamingServerHandler(request, responseStream, context, continuation).ConfigureAwait(false);
    }

    private IDisposable BeginCorrelation(Metadata metadata)
    {
        var correlations = new List<IDisposable>();

        foreach (var entry in metadata)
        {
            var key = entry.Key;
            var value = entry.Value;

            var correlationId = Correlation.AllIds.FirstOrDefault(correlationId => string.Equals(correlationId, key, StringComparison.OrdinalIgnoreCase));
            if (correlationId != null)
            {
                var correlation = _contextCorrelator.BeginLoggingCorrelationScope(correlationId, value);
                correlations.Add(correlation);
            }
        }
        return new CompositeDisposable(correlations);
    }
}
