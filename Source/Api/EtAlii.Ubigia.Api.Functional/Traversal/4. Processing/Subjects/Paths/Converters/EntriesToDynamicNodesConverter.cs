// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System.Collections.Generic;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Logical;

public class EntriesToDynamicNodesConverter : IEntriesToDynamicNodesConverter
{
    private readonly IScriptProcessingContext _context;

    public EntriesToDynamicNodesConverter(IScriptProcessingContext context)
    {
        _context = context;
    }

    public async IAsyncEnumerable<Node> Convert(IEnumerable<IReadOnlyEntry> entries, ExecutionScope scope)
    {
        foreach (var entry in entries)
        {
            // We want retrieved nodes to have the properties assigned, right?
            var properties = await _context.Logical.Properties.Get(entry.Id, scope).ConfigureAwait(false) ?? new PropertyDictionary();
            yield return new Node(entry, properties);
        }
    }

    public async Task<Node> Convert(IReadOnlyEntry entry, ExecutionScope scope)
    {
        // We want retrieved nodes to have the properties assigned, right?
        var properties = await _context.Logical.Properties.Get(entry.Id, scope).ConfigureAwait(false) ?? new PropertyDictionary();
        return new Node(entry, properties);
    }
}
