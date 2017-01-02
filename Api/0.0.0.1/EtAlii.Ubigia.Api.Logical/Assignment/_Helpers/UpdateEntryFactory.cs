namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    internal class UpdateEntryFactory : IUpdateEntryFactory
    {
        private readonly IAssignmentContext _context;

        public UpdateEntryFactory(IAssignmentContext context)
        {
            _context = context;
        }

        public async Task<IEditableEntry> Create(
            IReadOnlyEntry entry,
            ExecutionScope scope)
        {
            var newEntry = await _context.Fabric.Entries.Prepare();
            newEntry.Type = entry.Type;
            newEntry.Downdate = Relation.NewRelation(entry.Id);
            newEntry = (IEditableEntry)await _context.Fabric.Entries.Change(newEntry, scope);
            return newEntry;
        }
    }
}
