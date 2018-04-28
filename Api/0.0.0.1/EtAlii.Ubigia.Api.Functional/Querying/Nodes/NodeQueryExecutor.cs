namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using EtAlii.Ubigia.Api.Logical;
    using Remotion.Linq;

    // Called by re-linq when a query is to be executed.
    internal class NodeQueryExecutor : INodeQueryExecutor
    {
        private readonly ILogicalContext _logicalContext;
        private readonly IScriptParserFactory _scriptParserFactory;
        private readonly IScriptProcessorFactory _scriptProcessorFactory;
        private readonly INodeQueryModelVisitor _nodeQueryModelVisitor;
        private readonly IFunctionHandlersProvider _functionHandlersProvider;
        private readonly IRootHandlerMappersProvider _rootHandlerMappersProvider;

        public NodeQueryExecutor(
            ILogicalContext logicalContext,
            IScriptParserFactory scriptParserFactory,
            IScriptProcessorFactory scriptProcessorFactory,
            INodeQueryModelVisitor nodeQueryModelVisitor,
            IFunctionHandlersProvider functionHandlersProvider,
            IRootHandlerMappersProvider rootHandlerMappersProvider)
        {
            _logicalContext = logicalContext;
            _scriptParserFactory = scriptParserFactory;
            _scriptProcessorFactory = scriptProcessorFactory;
            _nodeQueryModelVisitor = nodeQueryModelVisitor;
            _functionHandlersProvider = functionHandlersProvider;
            _rootHandlerMappersProvider = rootHandlerMappersProvider;
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
            IEnumerable<T> result;

            var scope = new ScriptScope();

            _nodeQueryModelVisitor.VisitQueryModel(queryModel);
            var scriptText = _nodeQueryModelVisitor.GetScriptText();


            var scriptParserConfiguration = new ScriptParserConfiguration()
                .Use(_logicalContext.Configuration);
                //.Use(_diagnostics)
            var scriptParser = _scriptParserFactory.Create(scriptParserConfiguration);
            var scriptParseResult = scriptParser.Parse(scriptText);
            if (scriptParseResult.Errors.Any())
            {
                throw new NodeQueryingException("Unable to parse script needed to return a collection", scriptParseResult);
            }

            var scriptProcessorConfiguration = new ScriptProcessorConfiguration()
                .Use(scope)
                .Use(_logicalContext)
                .Use(_functionHandlersProvider)
                .Use(_rootHandlerMappersProvider);
            var scriptProcessor = _scriptProcessorFactory.Create(scriptProcessorConfiguration);

            // TODO: Attempt to make Linq async.
            var scriptResult = scriptProcessor
                .Process(scriptParseResult.Script)
                .ToEnumerable();

            var output = scriptResult
                .Last()
                .Output
                .ToEnumerable()
                .ToList();

            switch (_nodeQueryModelVisitor.ResultOperator)
            {
                case ResultOperator.Any:
                    result = new[] { (T)(object)output.Any() };
                    break;
                case ResultOperator.Count:
                    result = new[] { (T)(object)output.Count };
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
//
//        private void AddOutput<T>(List<object> output, object o)
//        {
//            // Only the final result is used.
//            output.Clear();
//
//            var enumerable = o as IEnumerable<object>;
//            if (enumerable != null)
//            {
//                output.AddRange(enumerable);
//            }
//            else if (o != null)
//            {
//                output.Add(o);
//            }
//        }
    }
}