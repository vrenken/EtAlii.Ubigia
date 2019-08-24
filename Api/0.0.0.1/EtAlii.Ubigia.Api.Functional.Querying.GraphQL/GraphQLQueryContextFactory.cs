namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using EtAlii.xTechnology.MicroContainer;

    public class GraphQLQueryContextFactory : Factory<IGraphQLQueryContext, GraphQLQueryContextConfiguration, IGraphQLQueryContextExtension>
    {
        protected override IScaffolding[] CreateScaffoldings(GraphQLQueryContextConfiguration configuration)
        {
            return new IScaffolding[]
            {
                new GraphQLQueryContextScaffolding(configuration), 
            };
        }
    }
}