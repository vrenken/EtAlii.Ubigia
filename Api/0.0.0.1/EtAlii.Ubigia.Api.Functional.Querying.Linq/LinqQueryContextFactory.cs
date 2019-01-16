namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    public class LinqQueryContextFactory
    {
        public ILinqQueryContext Create(ILinqQueryContextConfiguration configuration)
        {
            var container = new Container();
            
            var scaffoldings = new IScaffolding[]
            {
                new LinqQueryContextScaffolding(configuration), 
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
            
            foreach (var extension in configuration.Extensions)
            {
                extension.Initialize(container);
            }

            return container.GetInstance<ILinqQueryContext>();
        }
    }
}