// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#pragma warning disable // This is an external file.

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.ObjectPool;

    /// <summary>
    /// Provides extension methods for <see cref="IApplicationBuilder"/>.
    /// Reference:  https://github.com/aspnet/AspNetCore/blob/master/src/Hosting/Hosting/src/Internal/StartupLoader.cs
    /// </summary>
    public static class HostingExtensions
    {
        /// <summary>
        /// Creates a new isolated application builder which gets its own <see cref="ServiceCollection"/>, which only
        /// has the default services registered. It will not share the <see cref="ServiceCollection"/> from the
        /// originating app.
        /// </summary>
        /// <param name="app">The application builder to create the isolated app from.</param>
        /// <param name="configuration">The branch of the isolated app.</param>
        /// <param name="configureServices">A method to configure the newly created service collection.</param>
        /// <returns>The new pipeline with the isolated application integrated.</returns>
        public static IApplicationBuilder Isolate(
            this IApplicationBuilder app,
            Action<IApplicationBuilder> configuration,
            Action<IServiceCollection> configureServices)
        {
            return app.Isolate(configuration, services =>
            {
                configureServices(services);

                return services.BuildServiceProvider();
            });
        }

        /// <summary>
        /// Creates a new isolated application builder which gets its own <see cref="ServiceCollection"/>, which only
        /// has the default services registered. It will not share the <see cref="ServiceCollection"/> from the
        /// originating app.
        /// </summary>
        /// <param name="app">The application builder to create the isolated app from.</param>
        /// <param name="configuration">The branch of the isolated app.</param>
        /// <param name="configureServices">A method to configure the newly created service collection.</param>
        /// <returns>The new pipeline with the isolated application integrated.</returns>
        private static IApplicationBuilder Isolate(
            this IApplicationBuilder app,
            Action<IApplicationBuilder> configuration,
            Func<IServiceCollection, IServiceProvider> configureServices)
        {
            var services = CreateDefaultServiceCollection(app.ApplicationServices);
            var branchedServiceProvider = configureServices(services);

            var branchedApplicationBuilder = new ApplicationBuilder(branchedServiceProvider);
            branchedApplicationBuilder.EnableDependencyInjection(branchedServiceProvider);

            configuration(branchedApplicationBuilder);
            branchedApplicationBuilder.RunHostedServices().GetAwaiter().GetResult();

            return app.Use(next =>
            {
                // Run the rest of the pipeline in the original context,
                // with the services defined by the parent application builder.
                branchedApplicationBuilder.Run(async context =>
                {
                    var factory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

                    try
                    {
                        using var scope = factory.CreateScope();
                        context.RequestServices = scope.ServiceProvider;
                        await next(context).ConfigureAwait(false);
                    }

                    finally
                    {
                        context.RequestServices = null;
                    }
                });

                var branch = branchedApplicationBuilder.Build();

                return context => branch(context);
            });
        }

        /// <summary>
        /// This creates a new <see cref="ServiceCollection"/> with the same services registered as the
        /// <see cref="WebHostBuilder"/> does when creating a new <see cref="ServiceCollection"/>.
        /// </summary>
        /// <param name="provider">The service provider used to retrieve the default services.</param>
        /// <returns>A new <see cref="ServiceCollection"/> with the default services registered.</returns>
        [SuppressMessage("Sonar Code Smell", "S4792:Configuring loggers is security-sensitive", Justification = "Safe to do so here.")]
        private static ServiceCollection CreateDefaultServiceCollection(IServiceProvider provider)
        {
            var services = new ServiceCollection();

            // Copy the services added by the hosting layer (WebHostBuilder.BuildHostingServices).
            // See https://github.com/aspnet/Hosting/blob/dev/src/Microsoft.AspNetCore.Hosting/WebHostBuilder.cs.

            // SonarQube: Make sure that this logger's configuration is safe.
            // As we only add the logging services this ought to be safe. It is when and how they are configured that matters.
            services.AddLogging();

            if (provider.GetService<IHttpContextAccessor>() != null)
            {
                services.AddSingleton(provider.GetService<IHttpContextAccessor>());
            }

            services.AddSingleton(provider.GetRequiredService<IWebHostEnvironment>());
            services.AddSingleton(provider.GetRequiredService<ILoggerFactory>());
            services.AddSingleton(provider.GetRequiredService<IHostApplicationLifetime>());
            services.AddSingleton(_ => provider.GetRequiredService<IHttpContextFactory>());

            services.AddSingleton(provider.GetRequiredService<DiagnosticSource>());
            services.AddSingleton(provider.GetRequiredService<DiagnosticListener>());
            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddSingleton<HostedServiceExecutor>();

            return services;
        }

        public static IApplicationBuilder EnableDependencyInjection(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            app.Use(async (branchContext, next) =>
            {
                var factory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

                // Store the original request services in the current ASP.NET context.
                branchContext.Items[typeof(IServiceProvider)] = branchContext.RequestServices;

                try
                {
                    using var scope = factory.CreateScope();
                    branchContext.RequestServices = scope.ServiceProvider;
                    await next().ConfigureAwait(false);
                }

                finally
                {
                    branchContext.RequestServices = null;
                }
            });

            return app;
        }
    }
}
