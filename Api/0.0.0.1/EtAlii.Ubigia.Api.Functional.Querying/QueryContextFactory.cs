namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using EtAlii.xTechnology.MicroContainer;

    public class QueryContextFactory
    {
        public IQueryContext Create(IDataContext dataContext)
        {
            var container = new Container();
            
            container.Register<IQueryContext, QueryContext>();
            
            return container.GetInstance<IQueryContext>();
        }
    }
}