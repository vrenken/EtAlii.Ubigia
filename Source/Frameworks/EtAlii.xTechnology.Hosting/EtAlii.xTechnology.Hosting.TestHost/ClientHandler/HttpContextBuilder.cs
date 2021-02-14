namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting.Server;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;

    internal class HttpContextBuilder
    {
        private readonly IHttpApplication<HostingContext> _application;
        private readonly HttpContext _httpContext;

        private readonly TaskCompletionSource<HttpContext> _responseTcs = new(TaskCreationOptions.RunContinuationsAsynchronously);
        private readonly ResponseStream _responseStream;
        private readonly ResponseFeature _responseFeature = new();
        private readonly CancellationTokenSource _requestAbortedSource = new();
        private bool _pipelineFinished;
        private HostingContext _hostingContext;

        internal HttpContextBuilder(IHttpApplication<HostingContext> application)
        {
            _application = application ?? throw new ArgumentNullException(nameof(application));
            _httpContext = new DefaultHttpContext();

            var request = _httpContext.Request;
            request.Protocol = "HTTP/1.1";
            request.Method = HttpMethods.Get;

            _httpContext.Features.Set<IHttpResponseFeature>(_responseFeature);
            var requestLifetimeFeature = new HttpRequestLifetimeFeature {RequestAborted = _requestAbortedSource.Token};
            _httpContext.Features.Set<IHttpRequestLifetimeFeature>(requestLifetimeFeature);

            _responseStream = new ResponseStream(ReturnResponseMessageAsync, AbortRequest);
            _responseFeature.Body = _responseStream;
        }

        internal void Configure(Action<HttpContext> configureContext)
        {
            if (configureContext == null)
            {
                throw new ArgumentNullException(nameof(configureContext));
            }

            configureContext(_httpContext);
        }

        /// <summary>
        /// Start processing the request.
        /// </summary>
        /// <returns></returns>
        internal Task<HttpContext> SendAsync(CancellationToken cancellationToken)
        {
            var registration = cancellationToken.Register(AbortRequest);

            _hostingContext = _application.CreateContext(_httpContext.Features);

            // Async offload, don't let the test code block the caller.
            _ = Task.Factory.StartNew(async () =>
            {
                try
                {
                    await _application.ProcessRequestAsync(_hostingContext).ConfigureAwait(false);
                    await CompleteResponseAsync().ConfigureAwait(false);
                    _application.DisposeContext(_hostingContext, exception: null!);
                }
                catch (Exception ex)
                {
                    Abort(ex);
                    _application.DisposeContext(_hostingContext, ex);
                }
                finally
                {
                    await registration.DisposeAsync().ConfigureAwait(false);
                }
            }, cancellationToken);

            return _responseTcs.Task;
        }

        private void AbortRequest()
        {
            if (!_pipelineFinished)
            {
                _requestAbortedSource.Cancel();
            }
            _responseStream.CompleteWrites();
        }

        private async Task CompleteResponseAsync()
        {
            _pipelineFinished = true;
            await ReturnResponseMessageAsync().ConfigureAwait(false);
            _responseStream.CompleteWrites();
            await _responseFeature.FireOnResponseCompletedAsync().ConfigureAwait(false);
        }

        private async Task ReturnResponseMessageAsync()
        {
            // Check if the response has already started because the TrySetResult below could happen a bit late
            // (as it happens on a different thread) by which point the CompleteResponseAsync could run and calls this
            // method again.
            if (!_responseFeature.HasStarted)
            {
                // Sets HasStarted
                try
                {
                    await _responseFeature.FireOnSendingHeadersAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Abort(ex);
                    return;
                }

                // Copy the feature collection so we're not multi-threading on the same collection.
                var newFeatures = new FeatureCollection();
                foreach (var (key, value) in _httpContext.Features)
                {
                    newFeatures[key] = value;
                }
                _responseTcs.TrySetResult(new DefaultHttpContext(newFeatures));
            }
        }

        private void Abort(Exception exception)
        {
            _pipelineFinished = true;
            _responseStream.Abort(exception);
            _responseTcs.TrySetException(exception);
        }
    }
}
