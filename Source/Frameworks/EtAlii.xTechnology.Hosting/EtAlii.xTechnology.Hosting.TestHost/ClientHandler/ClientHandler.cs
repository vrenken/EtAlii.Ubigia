// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting.Server;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;

    /// <summary>
    /// This adapts HttpRequestMessages to ASP.NET Core requests, dispatches them through the pipeline, and returns the
    /// associated HttpResponseMessage.
    /// </summary>
    public class ClientHandler : HttpMessageHandler
    {
        private readonly IHttpApplication<HostingContext> _application;
        private readonly PathString _pathBase;

        /// <summary>
        /// Create a new handler.
        /// </summary>
        /// <param name="pathBase">The base path.</param>
        /// <param name="application">The <see cref="IHttpApplication{TContext}"/>.</param>
        public ClientHandler(PathString pathBase, IHttpApplication<HostingContext> application)
        {
            _application = application ?? throw new ArgumentNullException(nameof(application));

            // PathString.StartsWithSegments that we use below requires the base path to not end in a slash.
            if (pathBase.HasValue && pathBase.Value!.EndsWith("/"))
            {
                pathBase = new PathString(pathBase.Value.Substring(0, pathBase.Value.Length - 1));
            }
            _pathBase = pathBase;
        }

        /// <summary>
        /// This adapts HttpRequestMessages to ASP.NET Core requests, dispatches them through the pipeline, and returns the
        /// associated HttpResponseMessage.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return SendInternalAsync(request, cancellationToken);
        }

        /// <summary>
        /// This adapts HttpRequestMessages to ASP.NET Core requests, dispatches them through the pipeline, and returns the
        /// associated HttpResponseMessage.
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> SendInternalAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {
            var contextBuilder = new HttpContextBuilder(_application);

            Stream responseBody = null;
            var requestContent = requestMessage.Content;
            var body = requestContent != null
                ? await requestContent.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false)
                : null;
            contextBuilder.Configure(context => responseBody = ProcessRequest(requestMessage, context, requestContent, body));
            var httpContext = await contextBuilder.SendAsync(cancellationToken).ConfigureAwait(false);
            return BuildResponse(requestMessage, httpContext, responseBody);
        }

        private Stream ProcessRequest(
            HttpRequestMessage requestMessage,
            HttpContext context,
            HttpContent requestContent,
            Stream body)
        {
            var request = context.Request;

            request.Protocol = "HTTP/" + requestMessage.Version.ToString(fieldCount: 2);
            request.Method = requestMessage.Method.ToString();

            request.Scheme = requestMessage.RequestUri!.Scheme;

            foreach (var header in requestMessage.Headers)
            {
                request.Headers.Append(header.Key, header.Value.ToArray());
            }

            if (!request.Host.HasValue)
            {
                // If Host wasn't explicitly set as a header, let's infer it from the Uri
                request.Host = HostString.FromUriComponent(requestMessage.RequestUri);
                if (requestMessage.RequestUri.IsDefaultPort)
                {
                    request.Host = new HostString(request.Host.Host);
                }
            }

            request.Path = PathString.FromUriComponent(requestMessage.RequestUri);
            request.PathBase = PathString.Empty;
            if (request.Path.StartsWithSegments(_pathBase, out var remainder))
            {
                request.Path = remainder;
                request.PathBase = _pathBase;
            }

            request.QueryString = QueryString.FromUriComponent(requestMessage.RequestUri);

            if (requestContent != null)
            {
                foreach (var header in requestContent.Headers)
                {
                    request.Headers.Append(header.Key, header.Value.ToArray());
                }
            }

            if (body != null)
            {
                if (body.CanSeek)
                {
                    // This body may have been consumed before, rewind it.
                    body.Seek(0, SeekOrigin.Begin);
                }

                request.Body = body;
            }

            return context.Response.Body;
        }

        private static HttpResponseMessage BuildResponse(
            HttpRequestMessage requestMessage,
            HttpContext httpContext,
            Stream responseBody)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = (HttpStatusCode) httpContext.Response.StatusCode,
                ReasonPhrase = httpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase,
                RequestMessage = requestMessage,
                Content = new StreamContent(responseBody)
            };

            foreach (var header in httpContext.Response.Headers)
            {
                if (!response.Headers.TryAddWithoutValidation(header.Key, (IEnumerable<string>) header.Value))
                {
                    var success = response.Content.Headers.TryAddWithoutValidation(header.Key, (IEnumerable<string>) header.Value);
                    Contract.Assert(success, "Bad header");
                }
            }

            return response;
        }
    }
}
