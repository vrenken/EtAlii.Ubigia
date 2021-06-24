// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Collections;

    internal class GraphPathAllParentsRelationTraverser : IGraphPathAllParentsRelationTraverser
    {
        public void Configure(TraversalParameters parameters)
        {
            parameters.Input.SubscribeAsync(
                    onError: e => parameters.Output.OnError(e),
                    onNext: async start =>
                    {
                        var results = new List<Identifier>();
                        var path = new List<IReadOnlyEntry>();

                        var entry = await parameters.Context.Entries.Get(start, parameters.Scope).ConfigureAwait(false);

                        do
                        {
                            path.Add(entry);
                            var entries = await parameters.Context.Entries
                                .GetRelated(entry.Id, EntryRelation.Downdate, parameters.Scope)
                                .ToArrayAsync()
                                .ConfigureAwait(false);
                            if (entries.Multiple())
                            {
                                throw new NotSupportedException("The GraphPathAllParentsRelationTraverser is not able to process splitted temporal paths.");
                            }
                            entry = entries.SingleOrDefault();

                        } while (entry != null);

                        for (var i = path.Count; i > 0; i--)
                        {
                            entry = path[i - 1];

                            var children = parameters.Context.Entries.GetRelated(entry.Id, EntryRelation.Parent, parameters.Scope);
                            await foreach (var child in children.ConfigureAwait(false))
                            {
                                await Update(results, child, parameters.Context, parameters.Scope).ConfigureAwait(false);
                            }
                        }

                        foreach (var result in results)
                        {
                            parameters.Output.OnNext(result);
                        }
                    },
                    onCompleted: () => parameters.Output.OnCompleted());

        }

        public async IAsyncEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, IPathTraversalContext context, ExecutionScope scope)
        {
            var result = new List<Identifier>();
            var path = new List<IReadOnlyEntry>();

            var entry = await context.Entries.Get(start, scope).ConfigureAwait(false);

            do
            {
                path.Add(entry);
                var entries = await context.Entries
                    .GetRelated(entry.Id, EntryRelation.Downdate, scope)
                    .ToArrayAsync()
                    .ConfigureAwait(false);
                if (entries.Multiple())
                {
                    throw new NotSupportedException("The GraphPathAllParentsRelationTraverser is not able to process splitted temporal paths.");
                }
                entry = entries.SingleOrDefault();

            } while (entry != null);

            for (var i = path.Count; i > 0; i--)
            {
                entry = path[i - 1];

                var children = context.Entries.GetRelated(entry.Id, EntryRelation.Parent, scope);
                await foreach (var child in children.ConfigureAwait(false)) // We cannot yield here somehow as the update method both adds and removes items. 
                {
                    await Update(result, child, context, scope).ConfigureAwait(false);
                }
            }

            foreach (var item in result)
            {
                yield return item;
            }
        }


        private async Task Update(List<Identifier> list, IReadOnlyEntry entry, IPathTraversalContext context, ExecutionScope scope)
        {
            switch (entry.Type)
            {
                case EntryType.Add:
                    if (entry.Parent != Relation.None)
                    {
                        list.Add(entry.Parent.Id);
                    }
                    if (entry.Parent2 != Relation.None)
                    {
                        list.Add(entry.Parent2.Id);
                    }
                    break;
                case EntryType.Remove:
                    await Remove(list, entry.Parent, context, scope).ConfigureAwait(false);
                    await Remove(list, entry.Parent2, context, scope).ConfigureAwait(false);
                    break;
            }
        }

        private async Task Remove(List<Identifier> list, Relation relation, IPathTraversalContext context, ExecutionScope scope)
        {
            if (relation != Relation.None)
            {
                var idToRemove = relation.Id;
                var entry = await context.Entries.Get(idToRemove, scope).ConfigureAwait(false);
                if (entry.Downdate != Relation.None && !list.Remove(entry.Downdate.Id))
                {
                    await Remove(list, entry.Downdate, context, scope).ConfigureAwait(false);
                }
            }
        }
    }
}