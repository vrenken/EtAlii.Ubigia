﻿namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc.Authentication;
    using EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc.Spaces;
    using EtAlii.xTechnology.Hosting.Grpc;
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

            serviceDefinitions.Add(new AdminAuthenticationServiceDefinitionFactory().Create(infrastructure));
            serviceDefinitions.Add(new AdminSpaceServiceDefinitionFactory().Create(infrastructure));
            
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
