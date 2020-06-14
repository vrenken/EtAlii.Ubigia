namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
    using EtAlii.xTechnology.Hosting.Service.Grpc;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminGrpcService : GrpcServiceBase
    {
        public AdminGrpcService(IConfigurationSection configuration) 
            : base(configuration)
        {
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

            services.AddSingleton(svc => container.GetInstance<ISimpleAuthenticationVerifier>());
            services.AddSingleton(svc => container.GetInstance<ISimpleAuthenticationTokenVerifier>());
            services.AddSingleton(svc => container.GetInstance<ISimpleAuthenticationBuilder>());

            services.AddSingleton(svc => (AdminAuthenticationService) container.GetInstance<IAdminAuthenticationService>());
            services.AddSingleton(svc => (AdminStorageService) container.GetInstance<IAdminStorageService>());
            services.AddSingleton(svc => (AdminAccountService) container.GetInstance<IAdminAccountService>());
            services.AddSingleton(svc => (AdminSpaceService) container.GetInstance<IAdminSpaceService>());
            
            var authenticationTokenVerifier = container.GetInstance<ISimpleAuthenticationTokenVerifier>();
            
            services
                .AddGrpc()
                .AddServiceOptions<AdminStorageService>(options => options.Interceptors.Add<AccountAuthenticationInterceptor>(authenticationTokenVerifier))
                .AddServiceOptions<AdminAccountService>(options => options.Interceptors.Add<AccountAuthenticationInterceptor>(authenticationTokenVerifier))
                .AddServiceOptions<AdminSpaceService>(options => options.Interceptors.Add<AccountAuthenticationInterceptor>(authenticationTokenVerifier));
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
                });
        }
    }
}
