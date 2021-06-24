// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#pragma warning disable // This is an external file. 

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Predicate = System.Func<Microsoft.AspNetCore.Http.HttpContext, bool>;

    /// <summary>
    /// Provides extension methods for <see cref="IApplicationBuilder"/>.
    /// Reference:  https://github.com/aspnet/AspNetCore/blob/master/src/Hosting/Hosting/src/Internal/StartupLoader.cs
    /// </summary>
    public static class ApplicationBuilderIsolatedMapWhenForServiceExtensions
    {
        /// <summary>
        /// Branches the request pipeline in isolation yet based on the result of the given predicate.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="predicate">Invoked with the request environment to determine if the branch should be taken</param>
        /// <param name="appBuilder">Configures a branch to take</param>
        /// <param name="services">A method to configure the newly created service collection.</param>
        /// <returns></returns>
        public static IApplicationBuilder IsolatedMapWhen(
            this IApplicationBuilder app, 
            Predicate predicate,
            Action<IApplicationBuilder> appBuilder,
            Action<IServiceCollection> services)
        {
            return app.Isolate(builder => builder.MapWhen(predicate, appBuilder), services);
        }
    }
}
