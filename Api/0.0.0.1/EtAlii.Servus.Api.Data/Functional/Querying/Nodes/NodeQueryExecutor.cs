namespace EtAlii.Servus.Api.Data
{
    using Remotion.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // Called by re-linq when a query is to be executed.
    public class NodeQueryExecutor : IQueryExecutor
    {
        private readonly IDataConnection _connection;
        private readonly IScriptParser _scriptParser;
        private readonly IScriptProcessor _scriptProcessor;
        private readonly NodeQueryModelVisitor _nodeQueryModelVisitor;

        public NodeQueryExecutor(
            IDataConnection connection,
            IScriptParser scriptParser,
            IScriptProcessor scriptProcessor,
            NodeQueryModelVisitor nodeQueryModelVisitor)
        {
            _connection = connection;
            _scriptParser = scriptParser;
            _scriptProcessor = scriptProcessor;
            _nodeQueryModelVisitor = nodeQueryModelVisitor;
        }

        // Executes a query with a scalar result, i.e. a query that ends with a result operator such as Count, Sum, or Average.
        public T ExecuteScalar<T>(QueryModel queryModel)
        {
            return ExecuteCollection<T>(queryModel).Single();
        }

        // Executes a query with a single result object, i.e. a query that ends with a result operator such as First, Last, Single, Min, or Max.
        public T ExecuteSingle<T>(QueryModel queryModel, bool returnDefaultWhenEmpty)
        {
            var collection = ExecuteCollection<T>(queryModel); 
            return returnDefaultWhenEmpty 
                ? collection.SingleOrDefault()
                : collection.Single();
        }

        // Executes a query with a collection result.
        public IEnumerable<T> ExecuteCollection<T>(QueryModel queryModel)
        {
            var result = (IEnumerable<T>)null;

            _nodeQueryModelVisitor.VisitQueryModel(queryModel);
            var scriptText = _nodeQueryModelVisitor.GetScriptText();

            var script = _scriptParser.Parse(scriptText);

            var output = new List<object>();
            _scriptProcessor.Process(script, new ScriptScope(o => AddOutput<T>(output,o)), _connection);

            switch(_nodeQueryModelVisitor.ResultOperator)
            {
                case ResultOperator.Any:
                    result = new T[] { (T)(object)output.Any() };
                    break;
                case ResultOperator.Count:
                    result = new T[] { (T)(object)output.Count() };
                    break;
                case ResultOperator.Cast:
                    result = output.Cast<IInternalNode>().Select(node => (T)Activator.CreateInstance(typeof(T), node.Entry));
                    break;
                default:
                    result = output.Cast<T>();
                    break;
            }
            return result;
        }

        private void AddOutput<T>(List<object> output, object o)
        {
            var nodes = o as IEnumerable<DynamicNode>;
            if (nodes != null)
            {
                output.AddRange(nodes);
            }
            else if (o != null)
            {
                output.Add(o);
            }
        }
    }
}