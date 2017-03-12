namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    internal class GraphUpdater : IGraphUpdater
    {
        private readonly IComposeContext _context;

        public GraphUpdater(IComposeContext context)        
        {
            _context = context;
        }

        public async Task<IEditableEntry> Update(IReadOnlyEntry entry, string newType, ExecutionScope scope)
        {
            var updateEntry = await _context.Fabric.Entries.Prepare();
            updateEntry.Type = newType;
            updateEntry.Downdate = Relation.NewRelation(entry.Id);
            updateEntry = (IEditableEntry)await _context.Fabric.Entries.Change(updateEntry, scope);
            return updateEntry;
        }

        public async Task<IEditableEntry> Update(IReadOnlyEntry entry, ExecutionScope scope)
        {
            return await Update(entry, entry.Type, scope);
        }
    }
}
