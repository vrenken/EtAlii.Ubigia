namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using System.Linq.Expressions;
    using EtAlii.xTechnology.MicroContainer;
    using Remotion.Linq;

    public class LinqQueryContextFactory
    {
        public ILinqQueryContext Create(IDataContext dataContext)
        {
            var container = new Container();
            
            var scaffoldings = new IScaffolding[]
            {
                new LinqQueryContextScaffolding(dataContext), 
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
            
            return container.GetInstance<ILinqQueryContext>();
        }
    }
}