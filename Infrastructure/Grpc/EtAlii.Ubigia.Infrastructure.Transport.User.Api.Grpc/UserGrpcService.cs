namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
	using System.Linq;
	using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
	using EtAlii.xTechnology.Hosting.Grpc;
	using EtAlii.xTechnology.MicroContainer;
	using global::Grpc.Core;
	using Microsoft.Extensions.Configuration;

	public class UserGrpcService : GrpcServiceBase
    {
	    public UserGrpcService(IConfigurationSection configuration) 
            : base(configuration)
	    {
	    }

	    protected override void OnConfigureServices(Server.ServiceDefinitionCollection serviceDefinitions)
        {
	        var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;

	        var container = new Container();
	        new UserApiScaffolding(infrastructure).Register(container);
	        new AuthenticationScaffolding().Register(container);     
	        new SerializationScaffolding().Register(container);

	        container.Register<IAccountAuthenticationInterceptor, AccountAuthenticationInterceptor>();
	        container.Register<ISpaceAuthenticationInterceptor, SpaceAuthenticationInterceptor>();
	        
	        var spaceAuthenticationInterceptor = container.GetInstance<ISpaceAuthenticationInterceptor>();
	        var accountAuthenticationInterceptor = container.GetInstance<IAccountAuthenticationInterceptor>();

	        serviceDefinitions.Add(new UserAuthenticationServiceDefinitionFactory().Create(infrastructure));
	        serviceDefinitions.Add(new UserStorageServiceDefinitionFactory().Create(infrastructure, accountAuthenticationInterceptor));
	        serviceDefinitions.Add(new UserAccountServiceDefinitionFactory().Create(infrastructure, accountAuthenticationInterceptor));
	        serviceDefinitions.Add(new UserSpaceServiceDefinitionFactory().Create(infrastructure, spaceAuthenticationInterceptor));
	        serviceDefinitions.Add(new UserRootServiceDefinitionFactory().Create(infrastructure, spaceAuthenticationInterceptor));
	        serviceDefinitions.Add(new UserEntryServiceDefinitionFactory().Create(infrastructure, spaceAuthenticationInterceptor));
	        serviceDefinitions.Add(new UserPropertiesServiceDefinitionFactory().Create(infrastructure, spaceAuthenticationInterceptor));
	        serviceDefinitions.Add(new UserContentServiceDefinitionFactory().Create(infrastructure, spaceAuthenticationInterceptor));
	        serviceDefinitions.Add(new UserContentDefinitionServiceDefinitionFactory().Create(infrastructure, spaceAuthenticationInterceptor));

            // TODO: GRPC
      //      var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure

      //      applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.User.Api.Grpc.BaseUrl,
      //          services =>
      //          {
	     //           services
						//.AddSingleton<ISpaceRepository>(infrastructure.Spaces)
						//.AddSingleton<IAccountRepository>(infrastructure.Accounts)
						//.AddSingleton<IRootRepository>(infrastructure.Roots)
						//.AddSingleton<IEntryRepository>(infrastructure.Entries)
		    //            .AddSingleton<IPropertiesRepository>(infrastructure.Properties)
		    //            .AddSingleton<IContentRepository>(infrastructure.Content)
		    //            .AddSingleton<IContentDefinitionRepository>(infrastructure.ContentDefinition)

		    //            .AddInfrastructureSimpleAuthentication(infrastructure)
		    //            .AddInfrastructureSerialization()

		    //            .AddCors()
		    //            .AddGrpc()
		    //            .AddJsonProtocol(options => SerializerFactory.Configure(options.PayloadSerializerSettings))
      //          },
      //          appBuilder =>
      //          {
      //              appBuilder
      //                  .UseCors(configuration =>
      //                  {
      //                      configuration.AllowAnyOrigin(); 
      //                  })
      //                  .UseGrpc(routes =>
      //                  {
      //                      routes.MapHub<AuthenticationHub>(GrpcHub.Authentication)

						//	routes.MapHub<AccountHub>(GrpcHub.Account)
						//	routes.MapHub<SpaceHub>(GrpcHub.Space)

						//	routes.MapHub<RootHub>(GrpcHub.Root)
      //                      routes.MapHub<EntryHub>(GrpcHub.Entry)
      //                      routes.MapHub<PropertiesHub>(GrpcHub.Property)
      //                      routes.MapHub<ContentHub>(GrpcHub.Content)
      //                      routes.MapHub<ContentDefinitionHub>(GrpcHub.ContentDefinition)
      //                  })
      //          })
        }
    }
}
