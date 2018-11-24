namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using global::GraphQL.Language.AST;
    using global::GraphQL.Types;

    internal class OperationProcessor : IOperationProcessor
    {
        private readonly INodesDirectiveHandler _nodesDirectiveHandler;
        private readonly IQueryFieldAdder _queryFieldAdder;

        public OperationProcessor(
            INodesDirectiveHandler nodesDirectiveHandler, 
            IQueryFieldAdder queryFieldAdder)
        {
            _nodesDirectiveHandler = nodesDirectiveHandler;
            _queryFieldAdder = queryFieldAdder;
        }

        public async Task<OperationRegistration> Process(Operation operation, IObjectGraphType query, Dictionary<System.Type, DynamicObjectGraphType> graphObjectInstances)
        {
            var directiveResults = new List<NodesDirective>();
            
            foreach (var directive in operation.Directives)
            {
                switch (directive.Name)
                {
                    case "nodes":     
                        var directiveResult = await _nodesDirectiveHandler.Handle(directive);
                        directiveResults.Add(directiveResult);
                      break;
                    default:
                        throw new NotSupportedException($"Unable to process directive '{directive.Name ?? "NULL"}'");
                }
            }
            
            var registration = OperationRegistration.FromDirectives(directiveResults);
            
            _queryFieldAdder.Add($"DirectiveType_{Guid.NewGuid():N}", directiveResults, registration, query, graphObjectInstances);
            
            return registration;
        }
    }
}