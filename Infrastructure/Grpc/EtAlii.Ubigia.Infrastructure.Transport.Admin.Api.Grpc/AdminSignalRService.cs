namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
    using EtAlii.xTechnology.Hosting;
    using global::Grpc.Core;
    using Microsoft.Extensions.Configuration;

    public class AdminGrpcService : GrpcServiceBase
    {
        public AdminGrpcService(IConfigurationSection configuration) 
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
    //        var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;

    //        applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.Admin.Api.Grpc.BaseUrl,
    //            services =>
    //            {
    //                services
    //                    .AddSingleton<IAccountRepository>(infrastructure.Accounts)
    //                    .AddSingleton<ISpaceRepository>(infrastructure.Spaces)
    //                    .AddSingleton<IStorageRepository>(infrastructure.Storages)

				//		.AddInfrastructureSimpleAuthentication(infrastructure)
				//		.AddInfrastructureSerialization()

	   //                 .AddCors()
    //                    .AddGrpc()
		  //              .AddJsonProtocol(options => SerializerFactory.Configure(options.PayloadSerializerSettings));
				//},
    //            appBuilder =>
    //            {
    //                appBuilder
    //                    .UseCors(configuration =>
    //                    {
    //                        configuration.AllowAnyOrigin();
    //                    })
    //                    .UseGrpc(routes =>
    //                    {
    //                        routes.MapHub<AuthenticationHub>(GrpcHub.Authentication);

    //                        routes.MapHub<StorageHub>(GrpcHub.Storage);
    //                        routes.MapHub<SpaceHub>(GrpcHub.Space);
    //                        routes.MapHub<AccountHub>(GrpcHub.Account);
				//		});
    //            });
        }
    }
}
