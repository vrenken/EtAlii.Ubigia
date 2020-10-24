namespace EtAlii.Ubigia.Provisioning.Mail
{
    public class MailProviderFactory : IProviderFactory
    {
        public IProvider Create(IProviderConfiguration configuration)
        {
            var container = new xTechnology.MicroContainer.Container();

            container.Register(() => configuration);
            container.Register(() => configuration.SystemScriptContext);
            container.Register(() => configuration.ManagementConnection);
            container.Register<IProviderContext, ProviderContext>();
            container.Register<IProvider, MailProvider>();

            container.Register<IMailImporter, MailImporter>();
            
            return container.GetInstance<IProvider>();
        }
    }
}
