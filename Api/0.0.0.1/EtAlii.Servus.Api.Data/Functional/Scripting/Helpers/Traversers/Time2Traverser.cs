namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Linq;

    internal class Time2Traverser : ITime2Traverser
    {
        private readonly IDataConnection _connection;

        public Time2Traverser(IDataConnection connection)
        {
            _connection = connection;
        }

        public IReadOnlyEntry Traverse(IReadOnlyEntry entry)
        {
            while (true)
            {
                var updateEntries = _connection.Entries.GetMultiple(entry.Id, EntryRelation.Update);
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
