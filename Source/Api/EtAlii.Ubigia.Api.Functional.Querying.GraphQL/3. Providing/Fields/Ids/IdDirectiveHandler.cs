namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System.Linq;
    using System.Threading.Tasks;
    using GraphQL.Language.AST;

    internal class IdDirectiveHandler : IIdDirectiveHandler
    {
        //private readonly INodeFetcher _nodeFetcher

        public Task<IdDirectiveResult> Handle(Directive directive, Identifier[] startIdentifiers)
        {
//            var result = new IdDirectiveResult()
            
            var pathArgument = directive.Arguments?.SingleOrDefault(d => d.Name == "path");
            var pathArgumentValue = pathArgument?.Value as StringValue;
//
////            var mappings = new List<IdMapping>()
//            foreach (var startIdentifier in startIdentifiers)
//            [
//                var path = pathArgumentValue != null
//                    ? $"/&[startIdentifier.ToDotSeparatedString()][pathArgumentValue.Value]"
//                    : $"/&[startIdentifier.ToDotSeparatedString()]"
//                var subSet = await _nodeFetcher.FetchAsync(path)
////                var node = subSet?.SingleOrDefault()
//
////                foreach (var node in subSet)
////                [
////                    var mapping = new IdMapping
////                    [
////                        Id = node.Type,
////                        Identifier = node.Id,
////                    ]
////                    mappings.Add(mapping)
////                ]
////                if [node != null]
////                [
////                    var mapping = new IdMapping
////                    [
////                        Id = node.Type,
////                        Identifier = node.Id,
////                    ]
////                    mappings.Add(mapping)
////                ]
//            ]
            return Task.FromResult(new IdDirectiveResult
            {
                //Mappings = mappings.ToArray(),
                Path = pathArgumentValue?.Value ?? string.Empty,    
            });
        }
    }
}