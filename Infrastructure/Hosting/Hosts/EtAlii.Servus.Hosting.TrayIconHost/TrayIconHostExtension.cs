namespace EtAlii.Servus.Infrastructure.Hosting
{
    using SimpleInjector;
    using EtAlii.Servus.Infrastructure;

    public class TrayIconHostExtension : IHostExtension
    {
        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new TrayIconHostScaffolding(),
            };
            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}