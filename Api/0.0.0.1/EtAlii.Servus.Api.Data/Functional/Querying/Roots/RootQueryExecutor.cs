namespace EtAlii.Servus.Api.Data
{
    using Remotion.Linq;
    using System.Collections.Generic;
    using System.Linq;

    // Called by re-linq when a query is to be executed.
    public class RootQueryExecutor : IQueryExecutor
    {
        private readonly IDataConnection _connection;
        private readonly IScriptProcessor _scriptProcessor;

        public RootQueryExecutor(
            IDataConnection connection, 
            IScriptProcessor scriptProcessor)
        {
            _connection = connection;
            _scriptProcessor = scriptProcessor;
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
            //var visitor = new NodeQueryModelVisitor();
            //visitor.VisitQueryModel(queryModel);
            //var commandData = visitor.GetGqlCommand();

            //var script = commandData.CreateScript(_connection);
            var output = new List<T>();
            //_scriptProcessor.Process(script, new ScriptScope(o => output.Add((T)o)), _connection);
            return output;
        }
    }
}