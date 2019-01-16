namespace EtAlii.Ubigia.Infrastructure.Transport.User.Portal.Grpc
{
    using EtAlii.xTechnology.Hosting.Grpc;
    using global::Grpc.Core;
    using Microsoft.Extensions.Configuration;

    public class UserPortalControllerService : GrpcServiceBase
    {
        public UserPortalControllerService(IConfigurationSection configuration) : base(configuration)
        {
        }

        protected override void OnConfigureServices(Server.ServiceDefinitionCollection serviceDefinitions)
        {
            // TODO: GRPC
            //applicationBuilder.UseBranchWithServices(Port, AbsoluteUri.User.Portal.BaseUrl,
            //    services =>
            //    {
            //        services
            //        //    .AddSingleton<IAccountRepository>(infrastructure.Accounts)
            //        //    .AddSingleton<ISpaceRepository>(infrastructure.Spaces)
            //        //    .AddSingleton<IStorageRepository>(infrastructure.Storages)
            //            .AddMvcForTypedController<UserPortalController>();
            //    },
            //    appBuilder =>
            //    {
            //        appBuilder.UseMvc();
            //        appBuilder.UseWelcomePage();
            //    });
        }
    }
}
