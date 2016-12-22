namespace EtAlii.xTechnology.Workflow
{
    using SimpleInjector;
    using System.Linq;

    public class QueryProcessor : IQueryProcessor
    {
        private Container _container;

        public QueryProcessor(Container container)
        {
            _container = container;
        }

        public IQueryable<TResult> Process<TResult>(IQuery<TResult> query)
        {
            var handler = query.GetHandler(_container);
            return handler.Handle(query);
        }
    }
}
