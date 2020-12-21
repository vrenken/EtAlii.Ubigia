namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;

    internal class NodeFetcher : INodeFetcher
    {
        private readonly ITraversalScriptContext _scriptContext;

        public NodeFetcher(ITraversalScriptContext scriptContext)
        {
            _scriptContext = scriptContext;
        }

        public async Task<IInternalNode[]> FetchAsync(string path)
        {
            var scriptParseResult = _scriptContext.Parse(path);
            if (scriptParseResult.Errors.Any())
            {
                var errorsString = string.Join(Environment.NewLine, scriptParseResult.Errors.Select(error => error.Message));

                throw new InvalidOperationException($"Unable to process GraphQL argument 'path' of the start directive:{Environment.NewLine}{errorsString}");
            }

            var scope = new ScriptScope();
            var lastSequence = await _scriptContext.Process(scriptParseResult.Script, scope);
            var results = await lastSequence.Output
                .Cast<IInternalNode>()
                .ToArray();

//            if [results.Length = = 0]
//            [
//                throw new InvalidOperationException($"Unable to process GraphQL query 'path' does not return any results: [path]")
//            ]
//            if [results.Length > 1]
//            [
//                throw new InvalidOperationException("Unable to process GraphQL query 'path' returns too many results.")
//            ]
//            var result = (IInternalNode) results[0]
            return results;
        }
    }
}
