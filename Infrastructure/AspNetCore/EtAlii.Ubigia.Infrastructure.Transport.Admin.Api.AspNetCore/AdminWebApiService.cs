namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.AspNetCore
{
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;

    public class AdminWebApiService : AspNetCoreServiceBase
    {
        public AdminWebApiService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;

            applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.Admin.Api.BaseUrl,
                services =>
                {
                    services
                        .AddSingleton<IAccountRepository>(infrastructure.Accounts)
                        .AddSingleton<ISpaceRepository>(infrastructure.Spaces)
                        .AddSingleton<IStorageRepository>(infrastructure.Storages)
                        .AddMvcForTypedController<WebApiController>();
                        //.AddRazorOptions(options =>
                        //{
                        //    options.FileProviders.Add(new EmbeddedFileProvider(GetType().Assembly, GetType().Namespace));
                        //});
                },
                appBuilder => appBuilder.UseMvc());
        }
    }
}
