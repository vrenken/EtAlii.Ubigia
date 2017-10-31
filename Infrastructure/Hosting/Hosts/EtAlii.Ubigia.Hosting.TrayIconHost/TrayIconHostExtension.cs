namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.xTechnology.MicroContainer;

    public class TrayIconHostExtension : IHostExtension
    {
        public void Register(Container container)
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