namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class NodeFetcher : INodeFetcher
    {
        private readonly IScriptsSet _scriptsSet;

        public NodeFetcher(IScriptsSet scriptsSet)
        {
            _scriptsSet = scriptsSet;
        }

        public async Task<IEnumerable<IInternalNode>> FetchAsync(string path)
        {
            var scriptParseResult = _scriptsSet.Parse(path);
            if (scriptParseResult.Errors.Any())
            {
                var errorsString = String.Join(Environment.NewLine, scriptParseResult.Errors.Select(error => error.Message));
                
                throw new InvalidOperationException($"Unable to process GraphQL argument 'path' of the start directive:{Environment.NewLine}{errorsString}");
            }

            var scope = new ScriptScope();
            var lastSequence = await _scriptsSet.Process(scriptParseResult.Script, scope);
            var results = await lastSequence.Output
                .Cast<IInternalNode>()
                .ToArray();
            
//            if (results.Length == 0)
//            {
//                throw new InvalidOperationException($"Unable to process GraphQL query 'path' does not return any results: {path}");
//            }

//            if (results.Length > 1)
//            {
//                throw new InvalidOperationException("Unable to process GraphQL query 'path' returns too many results.");
//            }

//            var result = (IInternalNode) results[0];
            return results;
        }
    }
}