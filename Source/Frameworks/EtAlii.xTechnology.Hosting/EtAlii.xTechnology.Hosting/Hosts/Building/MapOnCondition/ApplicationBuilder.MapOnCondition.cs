// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting;

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Predicate = System.Func<Microsoft.AspNetCore.Http.HttpContext, bool>;

/// <summary>
/// Extension methods for the <see cref="Microsoft.AspNetCore.Builder.Extensions.MapWhenMiddleware"/>.
/// </summary>
public static class MapWhenExtensions
{
    /// <summary>
    /// Branches the request pipeline based on the result of the given predicate.
    /// </summary>
    /// <param name="application"></param>
    /// <param name="environment"></param>
    /// <param name="pathMatch"></param>
    /// <param name="predicate">Invoked with the request environment to determine if the branch should be taken</param>
    /// <param name="configuration">Configures a branch to take</param>
    /// <returns></returns>
    public static IApplicationBuilder MapOnCondition(this IApplicationBuilder application, IWebHostEnvironment environment, PathString pathMatch, Predicate predicate, Action<IApplicationBuilder, IWebHostEnvironment> configuration)
    {
        if (application == null)
        {
            throw new ArgumentNullException(nameof(application));
        }

        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }
        if (pathMatch.HasValue && pathMatch.Value!.EndsWith("/", StringComparison.Ordinal))
        {
            throw new ArgumentException("The path must not end with a '/'", nameof(pathMatch));
        }

        // create branch
        var branchBuilder = application.New();
        configuration(branchBuilder, environment);
        var branch = branchBuilder.Build();

        // put middleware in pipeline
        var options = new MapOnConditionOptions
        {
            Predicate = predicate,
            Branch = branch,
            PathMatch = pathMatch,
        };
        return application.Use(next => new MapOnConditionMiddleware(next, options).Invoke);
    }
}
