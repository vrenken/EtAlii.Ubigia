namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    public class GraphQLQueryContextFactory
    {
        public IGraphQLQueryContext Create(IGraphQLQueryContextConfiguration configuration)
        {
            var container = new Container();
            
            var scaffoldings = new IScaffolding[]
            {
                new GraphQLQueryContextScaffolding(configuration), 
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
            
            foreach (var extension in configuration.Extensions)
            {
                extension.Initialize(container);
            }

            return container.GetInstance<IGraphQLQueryContext>();
        }
    }
}