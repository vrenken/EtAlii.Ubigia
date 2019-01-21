namespace EtAlii.Ubigia.Api.Functional
{
    using System.Collections.Generic;
    using System.Linq;
    using Remotion.Linq;

    // Called by re-linq when a query is to be executed.
    internal class RootQueryExecutor : IRootQueryExecutor 
    {
        private readonly IScriptProcessorFactory _scriptProcessorFactory;

        public RootQueryExecutor(IScriptProcessorFactory scriptProcessorFactory)
        {
            _scriptProcessorFactory = scriptProcessorFactory;
        }

        // Executes a query with a scalar result, i.e. a query that ends with a result operator such as Count, Sum, or Average.
        public T ExecuteScalar<T>(QueryModel queryModel)
        {
            return ExecuteCollection<T>(queryModel).Single();
        }

        // Executes a query with a single result object, i.e. a query that ends with a result operator such as First, Last, Single, Min, or Max.
        public T ExecuteSingle<T>(QueryModel queryModel, bool returnDefaultWhenEmpty)
        {
            return returnDefaultWhenEmpty ? ExecuteCollection<T>(queryModel).SingleOrDefault() : ExecuteCollection<T>(queryModel).Single();
        }

        // Executes a query with a collection result.
        public IEnumerable<T> ExecuteCollection<T>(QueryModel queryModel)
        {
            var output = new List<T>();
            return output;
        }
    }
}