﻿namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using global::GraphQL.Language.AST;
    using global::GraphQL.Types;

    internal class IdDirectiveHandler : IIdDirectiveHandler
    {
        private readonly INodeFetcher _nodeFetcher;

        public IdDirectiveHandler(INodeFetcher nodeFetcher)
        {
            _nodeFetcher = nodeFetcher;
        }      

        public async Task<IdDirectiveResult> Handle(Directive directive, Identifier[] startIdentifiers)
        {
            var result = new IdDirectiveResult();
            
            var pathArgument = directive.Arguments.SingleOrDefault(d => d.Name == "path");
            var pathArgumentValue = pathArgument?.Value as StringValue;

            var mappings = new List<IdMapping>();
            foreach (var startIdentifier in startIdentifiers)
            {
                var path = pathArgumentValue != null
                    ? $"/&{startIdentifier.ToDotSeparatedString()}{pathArgumentValue.Value}"
                    : $"/&{startIdentifier.ToDotSeparatedString()}";
                var subSet = await _nodeFetcher.FetchAsync(path);
                var node = subSet?.SingleOrDefault();

                if (node != null)
                {
                    var mapping = new IdMapping
                    {
                        Id = node.Type,
                        Identifier = node.Id,
                    };
                    mappings.Add(mapping);
                }
            }
            
            return new IdDirectiveResult
            {
                Mappings = mappings.ToArray(),
                Path = pathArgumentValue?.Value ?? String.Empty,    
            };;
        }
    }
}