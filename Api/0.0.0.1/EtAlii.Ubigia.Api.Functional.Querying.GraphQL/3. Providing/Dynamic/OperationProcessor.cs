namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::GraphQL.Language.AST;
    using global::GraphQL.Types;

    internal class OperationProcessor : IOperationProcessor
    {
        private readonly IFieldTypeBuilder _fieldTypeBuilder;
        private readonly INodeFetcher _nodeFetcher;
        private readonly ITraverseDirectiveHandler _traverseDirectiveHandler;

        public OperationProcessor(
            IFieldTypeBuilder fieldTypeBuilder, 
            INodeFetcher nodeFetcher, 
            ITraverseDirectiveHandler traverseDirectiveHandler)
        {
            _fieldTypeBuilder = fieldTypeBuilder;
            _nodeFetcher = nodeFetcher;
            _traverseDirectiveHandler = traverseDirectiveHandler;
        }

        public async Task<OperationRegistration> Process(Operation operation, IObjectGraphType query, Dictionary<System.Type, DynamicObjectGraphType> graphObjectInstances)
        {
            var directiveResults = new List<TraverseDirective>();
            
            foreach (var directive in operation.Directives)
            {
                switch (directive.Name)
                {
                    case "traverse":
                        var directiveResult = await _traverseDirectiveHandler.Handle(directive, query, graphObjectInstances);
                        directiveResults.Add(directiveResult);
                      break;
                    default:
                        throw new NotSupportedException($"Unable to process directive '{directive.Name ?? "NULL"}'");
                }
            }
            
            return OperationRegistration.FromDirectives(directiveResults);
        }
    }
}