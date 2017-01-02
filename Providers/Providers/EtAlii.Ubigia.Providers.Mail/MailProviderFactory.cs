namespace EtAlii.Ubigia.Provisioning.Mail
{
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Management;
    using EtAlii.xTechnology.Logging;

    public class MailProviderFactory : IProviderFactory
    {
        public IProvider Create(IProviderConfiguration configuration)
        {
            var container = new xTechnology.MicroContainer.Container();

            container.Register<IProviderConfiguration>(() => configuration);
            container.Register<IDataContext>(() => configuration.SystemDataContext);
            container.Register<IManagementConnection>(() => configuration.ManagementConnection);
            container.Register<IProviderContext, ProviderContext>();
            container.Register<IProvider, MailProvider>();

            container.Register<IMailImporter, MailImporter>();

            container.Register<ILogger>(() => configuration.LogFactory.Create("Mail", "Provider"));

            return container.GetInstance<IProvider>();
        }
    }
}
