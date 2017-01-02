//namespace EtAlii.Servus.Api.Functional
//{
//    using System.Threading.Tasks;

//    internal class UpdateEntryFactory2 : IUpdateEntryFactory2
//    {
//        private readonly IProcessingContext _context;

//        public UpdateEntryFactory2(IProcessingContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEditableEntry> Create(
//            IReadOnlyEntry entry, 
//            ExecutionScope scope)
//        {
//            var newEntry = await _context.Logical.Nodes.Prepare();
//            newEntry.Type = entry.Type;
//            newEntry.Downdate = Relation.NewRelation(entry.Id);
//            newEntry = (IEditableEntry)await _context.Logical.Nodes.Change(newEntry, scope);
//            return newEntry;
//        }
//    }
//}
