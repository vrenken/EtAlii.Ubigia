namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest.AspNetCore
{
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class UserRestService : AspNetCoreServiceBase
    {
        public UserRestService(IConfigurationSection configuration) : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;

            applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.User.Api.Rest.BaseUrl,
                services =>
                {
                    services
                        .AddSingleton<IRootRepository>(infrastructure.Roots)
                        .AddSingleton<IEntryRepository>(infrastructure.Entries)
                        .AddSingleton<IPropertiesRepository>(infrastructure.Properties)
                        .AddSingleton<IContentRepository>(infrastructure.Content)
                        .AddSingleton<IContentDefinitionRepository>(infrastructure.ContentDefinition)
                        .AddMvcForTypedController<RestController>();
                },
                appBuilder => appBuilder.UseMvc());
        }
    }
}
