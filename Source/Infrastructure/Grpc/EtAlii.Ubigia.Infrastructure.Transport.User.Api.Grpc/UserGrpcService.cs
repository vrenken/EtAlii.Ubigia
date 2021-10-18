// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using System;
	using EtAlii.Ubigia.Infrastructure.Transport.Grpc;using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.DependencyInjection;
    using EtAlii.xTechnology.Threading;
    using Microsoft.AspNetCore.Hosting;
    using Serilog;
    using IServiceCollection = Microsoft.Extensions.DependencyInjection.IServiceCollection;

    public class UserGrpcService : INetworkService
    {
        private readonly ILogger _logger = Log.ForContext<UserGrpcService>();

        /// <inheritdoc />
        public Status Status { get; }

        /// <inheritdoc />
        public ServiceConfiguration Configuration { get; }

        public UserGrpcService(ServiceConfiguration configuration, Status status)
        {
            Configuration = configuration;
            Status = status;
            _logger.Information("Instantiated {ServiceName}", nameof(UserGrpcService));
        }

	    public void ConfigureServices(IServiceCollection services, IServiceProvider globalServices)
        {
            var infrastructure = globalServices.GetService<IInfrastructureService>()!.Infrastructure;

            var container = new Container();
            new UserApiScaffolding(infrastructure).Register(container);
            new AuthenticationScaffolding().Register(container);
            new SerializationScaffolding().Register(container);

            container.Register<IUserAuthenticationService, UserAuthenticationService>();
            container.Register<IUserStorageService, UserStorageService>();
            container.Register<IUserAccountService,UserAccountService>();
            container.Register<IUserSpaceService, UserSpaceService>();
            container.Register<IUserRootService, UserRootService>();
            container.Register<IUserEntryService, UserEntryService>();
            container.Register<IUserPropertiesService, UserPropertiesService>();
            container.Register<IUserContentService, UserContentService>();
            container.Register<IUserContentDefinitionService, UserContentDefinitionService>();

            services.AddSingleton(_ => container.GetInstance<ISimpleAuthenticationVerifier>());
            services.AddSingleton(_ => container.GetInstance<ISimpleAuthenticationTokenVerifier>());
            services.AddSingleton(_ => container.GetInstance<ISimpleAuthenticationBuilder>());

            services.AddSingleton(_ => (UserAuthenticationService) container.GetInstance<IUserAuthenticationService>());
            services.AddSingleton(_ => (UserStorageService) container.GetInstance<IUserStorageService>());
            services.AddSingleton(_ => (UserAccountService) container.GetInstance<IUserAccountService>());
            services.AddSingleton(_ => (UserSpaceService) container.GetInstance<IUserSpaceService>());
            services.AddSingleton(_ => (UserRootService) container.GetInstance<IUserRootService>());
            services.AddSingleton(_ => (UserEntryService) container.GetInstance<IUserEntryService>());
            services.AddSingleton(_ => (UserPropertiesService) container.GetInstance<IUserPropertiesService>());
            services.AddSingleton(_ => (UserContentService) container.GetInstance<IUserContentService>());
            services.AddSingleton(_ => (UserContentDefinitionService) container.GetInstance<IUserContentDefinitionService>());

            var authenticationTokenVerifier = container.GetInstance<ISimpleAuthenticationTokenVerifier>();
            var contextCorrelator = container.GetInstance<IContextCorrelator>();

            services
                .AddGrpc()
                .AddServiceOptions<UserAccountService>(options =>
                {
                    options.Interceptors.Add<AccountAuthenticationInterceptor>(authenticationTokenVerifier);
                    options.Interceptors.Add<CorrelationServiceInterceptor>(contextCorrelator);
                })
                .AddServiceOptions<UserSpaceService>(options =>
                {
                    options.Interceptors.Add<AccountAuthenticationInterceptor>(authenticationTokenVerifier);
                    options.Interceptors.Add<CorrelationServiceInterceptor>(contextCorrelator);
                })
                .AddServiceOptions<UserRootService>(options =>
                {
                    options.Interceptors.Add<SpaceAuthenticationInterceptor>(authenticationTokenVerifier);
                    options.Interceptors.Add<CorrelationServiceInterceptor>(contextCorrelator);
                })
                .AddServiceOptions<UserEntryService>(options =>
                {
                    options.Interceptors.Add<SpaceAuthenticationInterceptor>(authenticationTokenVerifier);
                    options.Interceptors.Add<CorrelationServiceInterceptor>(contextCorrelator);
                })
                .AddServiceOptions<UserPropertiesService>(options =>
                {
                    options.Interceptors.Add<SpaceAuthenticationInterceptor>(authenticationTokenVerifier);
                    options.Interceptors.Add<CorrelationServiceInterceptor>(contextCorrelator);
                })
                .AddServiceOptions<UserContentService>(options =>
                {
                    options.Interceptors.Add<SpaceAuthenticationInterceptor>(authenticationTokenVerifier);
                    options.Interceptors.Add<CorrelationServiceInterceptor>(contextCorrelator);
                })
                .AddServiceOptions<UserContentDefinitionService>(options =>
                {
                    options.Interceptors.Add<SpaceAuthenticationInterceptor>(authenticationTokenVerifier);
                    options.Interceptors.Add<CorrelationServiceInterceptor>(contextCorrelator);
                });
        }

        public void ConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            application
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapGrpcService<UserAuthenticationService>();
                    endpoints.MapGrpcService<UserStorageService>();
                    endpoints.MapGrpcService<UserAccountService>();
                    endpoints.MapGrpcService<UserSpaceService>();
                    endpoints.MapGrpcService<UserRootService>();
                    endpoints.MapGrpcService<UserEntryService>();
                    endpoints.MapGrpcService<UserPropertiesService>();
                    endpoints.MapGrpcService<UserContentService>();
                    endpoints.MapGrpcService<UserContentDefinitionService>();
                });
        }
    }
}
