namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Collections;

    internal class GraphPathChildrenRelationTraverser : IGraphPathChildrenRelationTraverser
    {
        public void Configure(TraversalParameters parameters)
        {
            parameters.Input.SubscribeAsync(
                    onError: e => parameters.Output.OnError(e),
                    onNext: async start =>
                    {
                        var path = new List<IReadOnlyEntry>();
                        var results = new List<Identifier>();

                        var entry = await parameters.Context.Entries.Get(start, parameters.Scope);

                        do
                        {
                            path.Add(entry);
                            var entries = await parameters.Context.Entries
                                .GetRelated(entry.Id, EntryRelation.Downdate, parameters.Scope)
                                .ToArrayAsync();
                            if (entries.Multiple())
                            {
                                throw new NotSupportedException("The GraphPathChildRelationTraverser is not able to process splitted temporal paths.");
                            }
                            entry = entries.SingleOrDefault();

                        } while (entry != null);

                        for (var i = path.Count; i > 0; i--)
                        {
                            entry = path[i - 1];

                            var children = parameters.Context.Entries.GetRelated(entry.Id, EntryRelation.Child, parameters.Scope);
                            await foreach (var child in children)
                            {
                                await Update(results, child, parameters.Context, parameters.Scope);
                            }
                        }

                        foreach (var result in results)
                        {
                            parameters.Output.OnNext(result);
                        }
                    },
                    onCompleted: () => parameters.Output.OnCompleted());

        }

        public async IAsyncEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            var result = new List<Identifier>();
            var path = new List<IReadOnlyEntry>();

            var entry = await context.Entries.Get(start, scope);

            do
            {
                path.Add(entry);
                var entries = await context.Entries
                    .GetRelated(entry.Id, EntryRelation.Downdate, scope)
                    .ToArrayAsync();
                if (entries.Multiple())
                {
                    throw new NotSupportedException("The GraphPathChildRelationTraverser is not able to process splitted temporal paths.");
                }
                entry = entries.SingleOrDefault();

            } while (entry != null);

            for (var i = path.Count; i > 0; i--)
            {
                entry = path[i - 1];

                var children = context.Entries.GetRelated(entry.Id, EntryRelation.Child, scope);
                await foreach (var child in children) // We cannot yield here somehow as the update method both adds and removes items. 
                {
                    await Update(result, child, context, scope);
                }
            }

            foreach (var item in result)
            {
                yield return item;
            }
        }

        private async Task Update(List<Identifier> list, IReadOnlyEntry entry, ITraversalContext context, ExecutionScope scope)
        {
            switch (entry.Type)
            {
                case EntryType.Add:
                    list.AddRangeOnce(entry.Children.Select(c => c.Id));
                    list.AddRangeOnce(entry.Children2.Select(c => c.Id)); 
                    break;
                case EntryType.Remove:
                    await Remove(list, entry.Children, context, scope);
                    await Remove(list, entry.Children2, context, scope);
                    break;
            }
        }

        private async Task Remove(List<Identifier> list, IEnumerable<Relation> relations, ITraversalContext context, ExecutionScope scope)
        {
            var idsToRemove = relations
                .Select(c => c.Id)
                .AsEnumerable();
            foreach (var idToRemove in idsToRemove)
            {
                var entry = await context.Entries.Get(idToRemove, scope);
                if (entry.Downdate != Relation.None && !list.Remove(entry.Downdate.Id))
                {
                    await Remove(list, new[] { entry.Downdate }, context, scope);
                }
            }
        }

        //private void Remove(List<Identifier> list, IEnumerable<Relation> relations)
        //[
        //    var ids = relations
        //        .Select(c => c.Id)
        //        .AsEnumerable()
        //    var toRemove = ids.SelectMany(id => _context.Fabric.Entries
        //        .GetRelated(id, EntryRelation.Downdate)
        //        .Select(c => c.Id))
        //    list.RemoveRange(toRemove)
        //]
    }
}