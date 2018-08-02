namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using System.Linq.Expressions;
    using EtAlii.xTechnology.MicroContainer;

    public class GraphQLQueryContextFactory
    {
        public IGraphQLQueryContext Create(IDataContext dataContext)
        {
            var container = new Container();
            
            var scaffoldings = new IScaffolding[]
            {
                new GraphQLQueryContextScaffolding(dataContext), 
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
            
            return container.GetInstance<IGraphQLQueryContext>();
        }
    }
}