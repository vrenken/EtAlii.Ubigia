namespace EtAlii.Servus.Provisioning.Hosting
{
    using EtAlii.Servus.Provisioning.Mail;
    using EtAlii.Servus.Provisioning.Time;
    using EtAlii.Servus.Provisioning.Twitter;
    using EtAlii.xTechnology.MicroContainer;

    public class ProviderComponentsScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<TimeComponent, TimeComponent>();
            container.Register<TwitterComponent, TwitterComponent>();
            container.Register<MailComponent, MailComponent>();
            container.Register<IProviderComponent[]>(() => new IProviderComponent[]
            {
                container.GetInstance<TimeComponent>(),
                container.GetInstance<TwitterComponent>(),
                container.GetInstance<MailComponent>(),
            });
        }
    }
}
