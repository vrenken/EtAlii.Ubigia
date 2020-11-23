#pragma warning disable // This is an external file. 

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder.Extensions;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Represents a middleware that runs a sub-request pipeline when a given predicate is matched.
    /// However, it uses Map instead of MapWhen, which has a big impact on the routing used:
    /// https://stackoverflow.com/questions/60108687/difference-between-userouter-and-useendpoints?noredirect=1
    /// </summary>
    public class MapOnConditionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly MapOnConditionOptions _options;

        /// <summary>
        /// Creates a new instance of <see cref="MapWhenMiddleware"/>.
        /// </summary>
        /// <param name="next">The delegate representing the next middleware in the request pipeline.</param>
        /// <param name="options">The middleware options.</param>
        public MapOnConditionMiddleware(RequestDelegate next, MapOnConditionOptions options)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Executes the middleware.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> for the current request.</param>
        /// <returns>A task that represents the execution of this middleware.</returns>
        public async Task Invoke(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (_options.Predicate(context) && 
                context.Request.Path.StartsWithSegments(_options.PathMatch, out var matchedPath, out var remainingPath))
            {
                // Update the path
                var path = context.Request.Path;
                var pathBase = context.Request.PathBase;
                context.Request.PathBase = pathBase.Add(matchedPath);
                context.Request.Path = remainingPath;

                try
                {
                    await _options.Branch(context).ConfigureAwait(false);
                }
                finally
                {
                    context.Request.PathBase = pathBase;
                    context.Request.Path = path;
                }
            }
            else
            {
                await _next(context).ConfigureAwait(false);
            }
        }
    }
}