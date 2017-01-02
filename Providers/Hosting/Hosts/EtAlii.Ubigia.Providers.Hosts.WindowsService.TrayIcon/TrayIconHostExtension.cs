namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using EtAlii.xTechnology.MicroContainer;

    public class TrayIconHostExtension : IHostExtension
    {
        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new TrayIconProviderScaffolding(),
//                new ProviderComponentsScaffolding(),
            };
            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}