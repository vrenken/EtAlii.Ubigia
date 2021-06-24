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
        /// If the request path starts with the given <paramref name="path"/>, execute the app configured via
        /// the configuration method of the <typeparamref name="TStartup"/> class instead of continuing to the next component
        /// in the pipeline. The new app will get an own newly created <see cref="ServiceCollection"/> and will not share
        /// the <see cref="ServiceCollection"/> of the originating app.
        /// </summary>
        /// <typeparam name="TStartup">The startup class used to configure the new app and the service collection.</typeparam>
        /// <param name="app">The application builder to register the isolated map with.</param>
        /// <param name="path">The path to match. Must not end with a '/'.</param>
        /// <returns>The new pipeline with the isolated middleware configured.</returns>
        public static IApplicationBuilder IsolatedMap<TStartup>(this IApplicationBuilder app, PathString path)
            where TStartup : class
        {
            var environment = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
            var methods = StartupLoader.LoadMethods(app.ApplicationServices, typeof(TStartup), environment.EnvironmentName);

            return app.IsolatedMap(path, methods.ConfigureDelegate, methods.ConfigureServicesDelegate);
        }

        /// <summary>
        /// If the request path starts with the given <paramref name="path"/>, execute the app configured via
        /// the configuration method of the <typeparamref name="TStartup"/> class instead of continuing to the next component
        /// in the pipeline. The new app will get an own newly created <see cref="ServiceCollection"/> and will not share
        /// the <see cref="ServiceCollection"/> of the originating app.
        /// </summary>
        /// <typeparam name="TStartup">The startup class used to configure the new app and the service collection.</typeparam>
        /// <param name="app">The application builder to register the isolated map with.</param>
        /// <param name="path">The path to match. Must not end with a '/'.</param>
        /// <param name="configureServices"></param>
        /// <returns>The new pipeline with the isolated middleware configured.</returns>
        public static IApplicationBuilder IsolatedMap<TStartup>(
            this IApplicationBuilder app,
            PathString path,
            Action<IServiceCollection> configureServices)
            where TStartup : class
        {
            var environment = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
            var methods = StartupLoader.LoadMethods(
                app.ApplicationServices,
                typeof(TStartup),
                environment.EnvironmentName);

            return app.IsolatedMap(path, methods.ConfigureDelegate, methods.ConfigureServicesDelegate);
        }

        /// <summary>
        /// If the request path starts with the given <paramref name="path"/>, execute the app configured via
        /// <paramref name="configuration"/> parameter instead of continuing to the next component in the pipeline.
        /// The new app will get an own newly created <see cref="ServiceCollection"/> and will not share the
        /// <see cref="ServiceCollection"/> from the originating app.
        /// </summary>
        /// <param name="app">The application builder to register the isolated map with.</param>
        /// <param name="path">The path to match. Must not end with a '/'.</param>
        /// <param name="configuration">The branch to take for positive path matches.</param>
        /// <param name="configureServices">A method to configure the newly created service collection.</param>
        /// <returns>The new pipeline with the isolated middleware configured.</returns>
        public static IApplicationBuilder IsolatedMap(
            this IApplicationBuilder app, PathString path,
            Action<IApplicationBuilder> configuration,
            Action<IServiceCollection> configureServices)
        {
            return app.IsolatedMap(path, configuration, services =>
            {
                configureServices(services);

                return services.BuildServiceProvider();
            });
        }

        /// <summary>
        /// If the request path starts with the given <paramref name="path"/>, execute the app configured via
        /// <paramref name="configuration"/> parameter instead of continuing to the next component in the pipeline.
        /// The new app will get an own newly created <see cref="ServiceCollection"/> and will not share the
        /// <see cref="ServiceCollection"/> from the originating app.
        /// </summary>
        /// <param name="app">The application builder to register the isolated map with.</param>
        /// <param name="path">The path to match. Must not end with a '/'.</param>
        /// <param name="configuration">The branch to take for positive path matches.</param>
        /// <param name="configureServices">A method to configure the newly created service collection.</param>
        /// <returns>The new pipeline with the isolated middleware configured.</returns>
        public static IApplicationBuilder IsolatedMap(
            this IApplicationBuilder app, PathString path,
            Action<IApplicationBuilder> configuration,
            Func<IServiceCollection, IServiceProvider> configureServices)
        {
            if (path.HasValue && path.Value!.EndsWith("/", StringComparison.Ordinal))
            {
                throw new ArgumentException("The path must not end with a '/'", nameof(path));
            }

            return app.Isolate(builder => builder.Map(path, configuration), configureServices);
        }

        /// <summary>
        /// Creates a new isolated application builder which gets its own <see cref="ServiceCollection"/>, which only
        /// has the default services registered. It will not share the <see cref="ServiceCollection"/> from the
        /// originating app. The isolated map will be configured using the configuration methods of the
        /// <typeparamref name="TStartup"/> class.
        /// </summary>
        /// <typeparam name="TStartup">The startup class used to configure the new app and the service collection.</typeparam>
        /// <param name="app">The application builder to create the isolated app from.</param>
        /// <returns>The new pipeline with the isolated application integrated.</returns>
        public static IApplicationBuilder Isolate<TStartup>(this IApplicationBuilder app) where TStartup : class
        {
            var environment = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
            var methods = StartupLoader.LoadMethods(app.ApplicationServices, typeof(TStartup), environment.EnvironmentName);

            return app.Isolate(methods.ConfigureDelegate, methods.ConfigureServicesDelegate);
        }

        /// <summary>
        /// Creates a new isolated application builder which gets its own <see cref="ServiceCollection"/>, which only
        /// has the default services registered. It will not share the <see cref="ServiceCollection"/> from the
        /// originating app.
        /// </summary>
        /// <param name="app">The application builder to create the isolated app from.</param>
        /// <param name="configuration">The branch of the isolated app.</param>
        /// <returns>The new pipeline with the isolated application integrated.</returns>
        public static IApplicationBuilder Isolate(
            this IApplicationBuilder app,
            Action<IApplicationBuilder> configuration)
        {
            return app.Isolate(configuration, services => services.BuildServiceProvider());
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
        public static IApplicationBuilder Isolate(
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

        internal static IApplicationBuilder EnableDependencyInjection(this IApplicationBuilder app, IServiceProvider serviceProvider)
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
