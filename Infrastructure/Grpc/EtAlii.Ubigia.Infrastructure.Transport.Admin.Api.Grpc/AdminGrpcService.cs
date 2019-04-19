namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
    using EtAlii.xTechnology.Hosting.Grpc;
    using EtAlii.xTechnology.MicroContainer;
    using global::Grpc.Core;
    using Microsoft.Extensions.Configuration;

    public class AdminGrpcService : GrpcServiceBase
    {
        public AdminGrpcService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureServices(Server.ServiceDefinitionCollection serviceDefinitions)
        {
            var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;

            var container = new Container();
            new AdminApiScaffolding(infrastructure).Register(container);
            new AuthenticationScaffolding().Register(container);     
            new SerializationScaffolding().Register(container);

            container.Register<IAccountAuthenticationInterceptor, AccountAuthenticationInterceptor>();
            //container.Register<ISpaceAuthenticationInterceptor, SpaceAuthenticationInterceptor>()
	        
            //var spaceAuthenticationInterceptor = container.GetInstance<ISpaceAuthenticationInterceptor>()
            var accountAuthenticationInterceptor = container.GetInstance<IAccountAuthenticationInterceptor>();

            serviceDefinitions.Add(new AdminAuthenticationServiceDefinitionFactory().Create(infrastructure));
            serviceDefinitions.Add(new AdminStorageServiceDefinitionFactory().Create(infrastructure, accountAuthenticationInterceptor));
            serviceDefinitions.Add(new AdminAccountServiceDefinitionFactory().Create(infrastructure));
            serviceDefinitions.Add(new AdminSpaceServiceDefinitionFactory().Create(infrastructure));
            
    //        var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure

    //        applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.Admin.Api.Grpc.BaseUrl,
    //            services =>
    //            [
    //                services
    //                    .AddSingleton<IAccountRepository>(infrastructure.Accounts)
    //                    .AddSingleton<ISpaceRepository>(infrastructure.Spaces)
    //                    .AddSingleton<IStorageRepository>(infrastructure.Storages)

				//		.AddInfrastructureSimpleAuthentication(infrastructure)
				//		.AddInfrastructureSerialization()

	   //                 .AddCors()
    //                    .AddGrpc()
		  //              .AddJsonProtocol(options => SerializerFactory.Configure(options.PayloadSerializerSettings))
				//},
    //            appBuilder =>
    //            [
    //                appBuilder
    //                    .UseCors(configuration =>
    //                    [
    //                        configuration.AllowAnyOrigin()
    //                    })
    //                    .UseGrpc(routes =>
    //                    [
    //                        routes.MapHub<AuthenticationHub>(GrpcHub.Authentication)

    //                        routes.MapHub<StorageHub>(GrpcHub.Storage)
    //                        routes.MapHub<SpaceHub>(GrpcHub.Space)
    //                        routes.MapHub<AccountHub>(GrpcHub.Account)
				//		})
    //            })
        }
    }
}
