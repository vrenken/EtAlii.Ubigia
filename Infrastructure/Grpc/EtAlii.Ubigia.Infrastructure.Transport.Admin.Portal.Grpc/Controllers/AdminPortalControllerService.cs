namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.Grpc
{
    using System.Diagnostics;
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Grpc.Builder;
    using Microsoft.Grpc.Http;
    using Microsoft.Grpc.Mvc.Controllers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminPortalControllerService : GrpcServiceBase
    {
        public AdminPortalControllerService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.Admin.Portal.BaseUrl,
                services =>
                {
                    services.AddMvcForTypedController<AdminPortalController>();
                    //.AddRazorOptions(options =>
                    //{
                    //    options.FileProviders.Add(new EmbeddedFileProvider(GetType().Assembly, GetType().Namespace));
                    //});
                },
                appBuilder => appBuilder.UseWelcomePage().UseMvc());
        }
    }
}
