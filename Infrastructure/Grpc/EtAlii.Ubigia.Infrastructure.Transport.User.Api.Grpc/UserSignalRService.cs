namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using EtAlii.xTechnology.Hosting;
    using global::Grpc.Core;
    using Microsoft.Extensions.Configuration;

    public class UserGrpcService : GrpcServiceBase
    {
        public UserGrpcService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }


        protected override void OnConfigureServer(Server server)
        {
            base.OnConfigureServer(server);
        }

        protected override void OnConfigureServices(Server.ServiceDefinitionCollection serviceDefinitions)
        {
            // TODO: GRPC
      //      var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;

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
		    //            .AddJsonProtocol(options => SerializerFactory.Configure(options.PayloadSerializerSettings));
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
      //                      routes.MapHub<AuthenticationHub>(GrpcHub.Authentication);

						//	routes.MapHub<AccountHub>(GrpcHub.Account);
						//	routes.MapHub<SpaceHub>(GrpcHub.Space);

						//	routes.MapHub<RootHub>(GrpcHub.Root);
      //                      routes.MapHub<EntryHub>(GrpcHub.Entry);
      //                      routes.MapHub<PropertiesHub>(GrpcHub.Property);
      //                      routes.MapHub<ContentHub>(GrpcHub.Content);
      //                      routes.MapHub<ContentDefinitionHub>(GrpcHub.ContentDefinition);
      //                  });
      //          });
        }
    }
}
