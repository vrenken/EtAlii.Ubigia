namespace EtAlii.Servus.Infrastructure.Hosting
{
    using Owin;
    using System.Web.Http;

    public abstract class InfrastructureComponent
    {
        public abstract void Setup(IAppBuilder application, HttpConfiguration webApiConfiguration);
    }
}