namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.Grpc
{
    using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
    using EtAlii.xTechnology.Hosting;
    using global::Grpc.Core;
    using Microsoft.Extensions.Configuration;

    public class AdminPortalControllerService : GrpcServiceBase
    {
        public AdminPortalControllerService(IConfigurationSection configuration) 
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
            //applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.Admin.Portal.BaseUrl,
            //    services =>
            //    {
            //        services.AddMvcForTypedController<AdminPortalController>();
            //        //.AddRazorOptions(options =>
            //        //{
            //        //    options.FileProviders.Add(new EmbeddedFileProvider(GetType().Assembly, GetType().Namespace));
            //        //});
            //    },
            //    appBuilder => appBuilder.UseWelcomePage().UseMvc());
        }
    }
}
