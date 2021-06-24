// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.Hosting.Service.Grpc;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using EtAlii.xTechnology.Threading;

    public class AdminGrpcService : GrpcServiceBase
    {
        private readonly IConfigurationDetails _configurationDetails;

        public AdminGrpcService(IConfigurationSection configuration, IConfigurationDetails configurationDetails)
            : base(configuration)
        {
            _configurationDetails = configurationDetails;
        }

        public override async Task Start()
        {
            Status.Title = "Ubigia infrastructure admin gRPC access";

            Status.Description = "Starting...";
            Status.Summary = "Starting Ubigia admin gRPC services";

            await base.Start().ConfigureAwait(false);

            var sb = new StringBuilder();
            sb.AppendLine("All OK. Ubigia admin gRPC services are available on the address specified below.");
            sb.AppendLine($"Address: {HostString}{PathString}");

            Status.Description = "Running";
            Status.Summary = sb.ToString();
        }

        public override async Task Stop()
        {
            Status.Description = "Stopping...";
            Status.Summary = "Stopping Ubigia admin gRPC services";

            await base.Stop().ConfigureAwait(false);

            var sb = new StringBuilder();
            sb.AppendLine("Finished providing Ubigia admin gRPC services on the address specified below.");
            sb.AppendLine($"Address: {HostString}{PathString}");

            Status.Description = "Stopped";
            Status.Summary = sb.ToString();
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;

            var container = new Container();
            new AdminApiScaffolding(infrastructure).Register(container);
            new AuthenticationScaffolding().Register(container);
            new SerializationScaffolding().Register(container);

            container.Register<IAdminAuthenticationService, AdminAuthenticationService>();
            container.Register<IAdminStorageService, AdminStorageService>();
            container.Register<IAdminAccountService, AdminAccountService>();
            container.Register<IAdminSpaceService, AdminSpaceService>();
            container.Register<IAdminInformationService, AdminInformationService>();

            container.Register(() => _configurationDetails);

            services.AddSingleton(_ => container.GetInstance<ISimpleAuthenticationVerifier>());
            services.AddSingleton(_ => container.GetInstance<ISimpleAuthenticationTokenVerifier>());
            services.AddSingleton(_ => container.GetInstance<ISimpleAuthenticationBuilder>());

            services.AddSingleton(_ => (AdminAuthenticationService) container.GetInstance<IAdminAuthenticationService>());
            services.AddSingleton(_ => (AdminStorageService) container.GetInstance<IAdminStorageService>());
            services.AddSingleton(_ => (AdminAccountService) container.GetInstance<IAdminAccountService>());
            services.AddSingleton(_ => (AdminSpaceService) container.GetInstance<IAdminSpaceService>());
            services.AddSingleton(_ => (AdminInformationService) container.GetInstance<IAdminInformationService>());

            var authenticationTokenVerifier = container.GetInstance<ISimpleAuthenticationTokenVerifier>();
            var contextCorrelator = container.GetInstance<IContextCorrelator>();

            services
                .AddGrpc()
                .AddServiceOptions<AdminStorageService>(options =>
                {
                    options.Interceptors.Add<AccountAuthenticationInterceptor>(authenticationTokenVerifier);
                    options.Interceptors.Add<CorrelationServiceInterceptor>(contextCorrelator);
                })
                .AddServiceOptions<AdminAccountService>(options =>
                {
                    options.Interceptors.Add<AccountAuthenticationInterceptor>(authenticationTokenVerifier);
                    options.Interceptors.Add<CorrelationServiceInterceptor>(contextCorrelator);
                })
                .AddServiceOptions<AdminSpaceService>(options =>
                {
                    options.Interceptors.Add<AccountAuthenticationInterceptor>(authenticationTokenVerifier);
                    options.Interceptors.Add<CorrelationServiceInterceptor>(contextCorrelator);
                })
                .AddServiceOptions<AdminInformationService>(options =>
                {
                    options.Interceptors.Add<AccountAuthenticationInterceptor>(authenticationTokenVerifier);
                    options.Interceptors.Add<CorrelationServiceInterceptor>(contextCorrelator);
                });
        }

        protected override void ConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapGrpcService<AdminAuthenticationService>();
                    endpoints.MapGrpcService<AdminStorageService>();
                    endpoints.MapGrpcService<AdminAccountService>();
                    endpoints.MapGrpcService<AdminSpaceService>();
                    endpoints.MapGrpcService<AdminInformationService>();
                });
        }
    }
}
