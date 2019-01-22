namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using global::GraphQL.Language.AST;

    internal class IdDirectiveHandler : IIdDirectiveHandler
    {
        private readonly INodeFetcher _nodeFetcher;

        public IdDirectiveHandler(INodeFetcher nodeFetcher)
        {
            _nodeFetcher = nodeFetcher;
        }      

        public async Task<IdDirectiveResult> Handle(Directive directive, Identifier[] startIdentifiers)
        {
            var pathArgument = directive.Arguments.SingleOrDefault(d => d.Name == "path");
            var pathArgumentValue = pathArgument?.Value as StringValue;

            return await Task.FromResult(new IdDirectiveResult
            {
                Path = pathArgumentValue?.Value ?? String.Empty,    
            });
        }
    }
}