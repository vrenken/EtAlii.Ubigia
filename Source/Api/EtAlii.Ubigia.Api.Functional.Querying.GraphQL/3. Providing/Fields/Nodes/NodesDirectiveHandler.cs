namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using GraphQL.Language.AST;

    internal class NodesDirectiveHandler : INodesDirectiveHandler
    {
        private readonly INodeFetcher _nodeFetcher;

        public NodesDirectiveHandler(INodeFetcher nodeFetcher)
        {
            _nodeFetcher = nodeFetcher;
        }      

        public async Task<NodesDirectiveResult> Handle(Directive directive, Identifier[] startIdentifiers = null)
        {
            var result = new NodesDirectiveResult();
            
            var argument = directive.Arguments.First();
            if (argument.Value is StringValue stringValue)
            {
                if (startIdentifiers?.Any() == true)
                {
                    var nodes = new List<IInternalNode>();
                    foreach (var startIdentifier in startIdentifiers)
                    {
                        var path = $"/&{startIdentifier.ToDotSeparatedString()}{stringValue.Value}";
                        var subSet = await _nodeFetcher.FetchAsync(path).ConfigureAwait(false);
                        nodes.AddRange(subSet);
                    }
                    result.Nodes = nodes.ToArray(); 
                }
                else
                {
                    result.Nodes = await _nodeFetcher.FetchAsync(stringValue.Value);
                }
                result.Path = stringValue.Value;    
            }
            return result;
        }
    }
}