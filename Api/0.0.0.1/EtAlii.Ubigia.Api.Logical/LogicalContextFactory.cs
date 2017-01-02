namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.xTechnology.MicroContainer;

    public class LogicalContextFactory 
    {
        public ILogicalContext Create(ILogicalContextConfiguration configuration)
        {
            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new ContextScaffolding(configuration),
                new GraphScaffolding(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in configuration.Extensions)
            {
                extension.Initialize(container);
            }

            return container.GetInstance<ILogicalContext>();
        }
    }
}