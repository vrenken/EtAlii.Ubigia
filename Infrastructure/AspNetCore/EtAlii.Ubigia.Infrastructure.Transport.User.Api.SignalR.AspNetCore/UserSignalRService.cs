﻿namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR.AspNetCore
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class UserSignalRService : AspNetCoreServiceBase
    {
        public UserSignalRService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;

            applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.User.Api.SignalR.BaseUrl,
                services =>
                {
	                services
		                .AddSingleton<IRootRepository>(infrastructure.Roots)
		                .AddSingleton<IEntryRepository>(infrastructure.Entries)
		                .AddSingleton<IPropertiesRepository>(infrastructure.Properties)
		                .AddSingleton<IContentRepository>(infrastructure.Content)
		                .AddSingleton<IContentDefinitionRepository>(infrastructure.ContentDefinition)

		                .AddInfrastructureSimpleAuthentication(infrastructure)
		                .AddInfrastructureSerialization()

		                .AddCors()
		                .AddSignalR()
		                .AddJsonProtocol(options => SerializerFactory.Configure(options.PayloadSerializerSettings));
                },
                appBuilder =>
                {
                    appBuilder
                        .UseCors(configuration =>
                        {
                            configuration.AllowAnyOrigin(); 
                        })
                        .UseSignalR(routes =>
                        {
                            routes.MapHub<AuthenticationHub>(SignalRHub.Authentication);

                            routes.MapHub<RootHub>(SignalRHub.Root);
                            routes.MapHub<EntryHub>(SignalRHub.Entry);
                            routes.MapHub<PropertiesHub>(SignalRHub.Property);
                            routes.MapHub<ContentHub>(SignalRHub.Content);
                            routes.MapHub<ContentDefinitionHub>(SignalRHub.ContentDefinition);
                        });
                });
        }
    }
}
