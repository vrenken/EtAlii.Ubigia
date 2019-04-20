﻿namespace EtAlii.Ubigia.Api.Functional
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
        private readonly INodeQueryModelVisitor _nodeQueryModelVisitor;
        private readonly IGraphSLScriptContext _scriptContext;
        
        public NodeQueryExecutor(INodeQueryModelVisitor nodeQueryModelVisitor, IGraphSLScriptContext scriptContext)
        {
            _nodeQueryModelVisitor = nodeQueryModelVisitor;
            _scriptContext = scriptContext;
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
            var scriptParseResult = _scriptContext.Parse(scriptText);
            if (scriptParseResult.Errors.Any())
            {
                throw new NodeQueryingException("Unable to parse script needed to return a collection", scriptParseResult);
            }

            // TODO: Attempt to make Linq async.
            var scriptResult = _scriptContext
                .Process(scriptParseResult.Script, scope)
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
//        [
//            // Only the final result is used.
//            output.Clear()
//
//            var enumerable = o as IEnumerable<object>
//            if [enumerable ! = null]
//            [
//                output.AddRange(enumerable)
//            ]
//            else if [o ! = null]
//            [
//                output.Add(o)
//            ]
//        ]
    }
}