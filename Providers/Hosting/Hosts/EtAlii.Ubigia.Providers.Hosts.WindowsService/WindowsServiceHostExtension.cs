namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using EtAlii.xTechnology.MicroContainer;

    public class WindowsServiceHostExtension : IHostExtension
    {
        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
//                new ProviderComponentsScaffolding(),
            };
            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}