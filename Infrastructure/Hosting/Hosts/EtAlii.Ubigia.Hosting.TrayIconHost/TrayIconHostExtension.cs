namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.xTechnology.MicroContainer;

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