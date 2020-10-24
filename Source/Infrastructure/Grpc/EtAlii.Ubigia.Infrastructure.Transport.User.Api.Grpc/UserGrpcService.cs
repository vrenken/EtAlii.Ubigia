namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
	using EtAlii.xTechnology.Hosting.Service.Grpc;
	using EtAlii.xTechnology.MicroContainer;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	public class UserGrpcService : GrpcServiceBase
    {
	    public UserGrpcService(IConfigurationSection configuration) 
            : base(configuration)
	    {
	    }

	    public override async Task Start()
	    {
		    Status.Title = "Ubigia infrastructure user gRPC access";

		    Status.Description = "Starting...";
		    Status.Summary = "Starting Ubigia user gRPC services";

		    await base.Start();

		    var sb = new StringBuilder();
		    sb.AppendLine("All OK. Ubigia user gRPC services are available on the address specified below.");
		    sb.AppendLine($"Address: {HostString}{PathString}");

		    Status.Description = "Running";
		    Status.Summary = sb.ToString();
	    }

	    public override async Task Stop()
	    {
		    Status.Description = "Stopping...";
		    Status.Summary = "Stopping Ubigia user gRPC services";

		    await base.Stop();

		    var sb = new StringBuilder();
		    sb.AppendLine("Finished providing Ubigia user gRPC services on the address specified below.");
		    sb.AppendLine($"Address: {HostString}{PathString}");

		    Status.Description = "Stopped";
		    Status.Summary = sb.ToString();
	    }

	    protected override void ConfigureServices(IServiceCollection services)
        {
	        var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;

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

            services.AddSingleton(svc => container.GetInstance<ISimpleAuthenticationVerifier>());
            services.AddSingleton(svc => container.GetInstance<ISimpleAuthenticationTokenVerifier>()); 
            services.AddSingleton(svc => container.GetInstance<ISimpleAuthenticationBuilder>());
            
            services.AddSingleton(svc => (UserAuthenticationService) container.GetInstance<IUserAuthenticationService>());
            services.AddSingleton(svc => (UserStorageService) container.GetInstance<IUserStorageService>());
            services.AddSingleton(svc => (UserAccountService) container.GetInstance<IUserAccountService>());
            services.AddSingleton(svc => (UserSpaceService) container.GetInstance<IUserSpaceService>());
            services.AddSingleton(svc => (UserRootService) container.GetInstance<IUserRootService>());
            services.AddSingleton(svc => (UserEntryService) container.GetInstance<IUserEntryService>());
            services.AddSingleton(svc => (UserPropertiesService) container.GetInstance<IUserPropertiesService>());
            services.AddSingleton(svc => (UserContentService) container.GetInstance<IUserContentService>());
            services.AddSingleton(svc => (UserContentDefinitionService) container.GetInstance<IUserContentDefinitionService>());
            
			var authenticationTokenVerifier = container.GetInstance<ISimpleAuthenticationTokenVerifier>();
            services
                .AddGrpc()
                .AddServiceOptions<UserAccountService>(options => options.Interceptors.Add<AccountAuthenticationInterceptor>(authenticationTokenVerifier))
                .AddServiceOptions<UserSpaceService>(options => options.Interceptors.Add<AccountAuthenticationInterceptor>(authenticationTokenVerifier))
                .AddServiceOptions<UserRootService>(options => options.Interceptors.Add<SpaceAuthenticationInterceptor>(authenticationTokenVerifier))
                .AddServiceOptions<UserEntryService>(options => options.Interceptors.Add<SpaceAuthenticationInterceptor>(authenticationTokenVerifier))
                .AddServiceOptions<UserPropertiesService>(options => options.Interceptors.Add<SpaceAuthenticationInterceptor>(authenticationTokenVerifier))
                .AddServiceOptions<UserContentService>(options => options.Interceptors.Add<SpaceAuthenticationInterceptor>(authenticationTokenVerifier))
                .AddServiceOptions<UserContentDefinitionService>(options => options.Interceptors.Add<SpaceAuthenticationInterceptor>(authenticationTokenVerifier));
        }

        protected override void ConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder
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
