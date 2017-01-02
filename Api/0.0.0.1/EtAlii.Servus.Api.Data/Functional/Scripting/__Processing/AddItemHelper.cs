namespace EtAlii.Servus.Api.Data
{
    internal class AddItemHelper : IAddItemHelper
    {
        private readonly IDataConnection _connection;

        public AddItemHelper(IDataConnection connection)
        {
            _connection = connection;
        }

        public DynamicNode AddNewEntry(AddItem action, IReadOnlyEntry entry)
        {
            IEditableEntry newEntry = null;
            foreach(NameComponent item in action.ItemPath.Components)
            {
                newEntry = _connection.Entries.Prepare();
                newEntry.Parent = Relation.NewRelation(entry.Id);
                newEntry.Type = item.Name;
                _connection.Entries.Change(newEntry);
                entry = (IReadOnlyEntry)newEntry;
            }
            return new DynamicNode((IReadOnlyEntry)newEntry);
        }
    }
}
