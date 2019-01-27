namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class GraphPathAllDowndatesRelationTraverser : RecursiveGraphPathTraverserBase, IGraphPathAllDowndatesRelationTraverser
    {
        protected override async Task<IEnumerable<Identifier>> GetNextRecursion(Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            var entries = await context.Entries
                .GetRelated(start, EntryRelation.Downdate, scope);
            return entries
                .Select(e => e.Id)
                .AsEnumerable();
        }
    }
}