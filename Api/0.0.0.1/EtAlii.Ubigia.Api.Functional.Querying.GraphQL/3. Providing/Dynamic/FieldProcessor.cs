namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using global::GraphQL.Language.AST;
    using global::GraphQL.Types;

    internal class FieldProcessor : IFieldProcessor
    {
        private readonly INodesDirectiveHandler _nodesDirectiveHandler;
        private readonly IQueryFieldAdder _queryFieldAdder;

        public FieldProcessor(
            INodesDirectiveHandler nodesDirectiveHandler, 
            IQueryFieldAdder queryFieldAdder)
        {
            _nodesDirectiveHandler = nodesDirectiveHandler;
            _queryFieldAdder = queryFieldAdder;
        }

        public async Task<FieldRegistration> Process(
            Field field, 
            Identifier[] startIdentifiers, 
            IObjectGraphType query, 
            Dictionary<System.Type, DynamicObjectGraphType> graphObjectInstances)
        {
            var directiveResults = new List<NodesDirective>();
            
            foreach (var directive in field.Directives)
            {
                switch (directive.Name)
                {
                    case "nodes":
                        var directiveResult = await _nodesDirectiveHandler.Handle(directive, startIdentifiers);
                        directiveResults.Add(directiveResult);
                      break;
                    default:
                        throw new NotSupportedException($"Unable to process directive '{directive.Name ?? "NULL"}'");
                }
            }
            
            var registration = FieldRegistration.FromDirectives(directiveResults);
            
             
            _queryFieldAdder.Add(field.Name, directiveResults, registration, query, graphObjectInstances);

            return registration;
        }
    }
}