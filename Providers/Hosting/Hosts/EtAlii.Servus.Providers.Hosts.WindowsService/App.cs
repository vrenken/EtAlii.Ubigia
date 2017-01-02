namespace EtAlii.Servus.Providers.Hosting.WindowsService
{
    using EtAlii.Servus.Infrastructure.Providers.Mail;
    using EtAlii.Servus.Providers.Time;
    using SimpleInjector;
    using System.Collections.Generic;
    using System.Configuration;

    public class App : EtAlii.Servus.Providers.App
    {
        protected override IProvisioningConfiguration GetConfiguration()
        {
            var exeConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            return (IProvisioningConfiguration)exeConfiguration.GetSection("provisioning");
        }

        protected override void RegisterComponents()
        {
            Container.Register<TimeComponent, TimeComponent>(Lifestyle.Singleton);
            Container.Register<TwitterComponent, TwitterComponent>(Lifestyle.Singleton);
            Container.Register<MailComponent, MailComponent>(Lifestyle.Singleton);
        }

        protected override IEnumerable<ProviderComponent> GetComponents(Container container)
        {
            return new ProviderComponent[]
            {
                container.GetInstance<TimeComponent>(),
                container.GetInstance<TwitterComponent>(),
                container.GetInstance<MailComponent>(),
            };
        }
    }
}
