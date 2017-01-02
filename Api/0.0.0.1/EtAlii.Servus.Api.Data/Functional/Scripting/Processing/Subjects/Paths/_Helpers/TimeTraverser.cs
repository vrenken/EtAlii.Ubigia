namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Linq;

    internal class TimeTraverser : ITimeTraverser
    {
        private readonly ProcessingContext _context;

        public TimeTraverser(ProcessingContext context)
        {
            _context = context;
        }

        public IReadOnlyEntry Traverse(IReadOnlyEntry entry)
        {
            while (true)
            {
                var updateEntries = _context.Connection.Entries.GetRelated(entry.Id, EntryRelation.Update);
                if (updateEntries.Any())
                {
                    if (updateEntries.Count() > 1)
                    {
                        var message = String.Format("Unable to traverse splitted temporal path: {0}",
                            entry.Id.ToString());
                        throw new InvalidOperationException(message);
                    }
                    else
                    {
                        entry = updateEntries.First();
                    }
                }
                else
                {
                    break;
                }
            }
            return entry;
        }
    }
}
