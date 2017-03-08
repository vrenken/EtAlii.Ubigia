namespace EtAlii.Ubigia.Api.Functional
{
    internal class RecursiveRemoveResult
    {
        private readonly Identifier _parentId;
        public readonly IEditableEntry NewEntry;

        public RecursiveRemoveResult(Identifier parentId, IEditableEntry newEntry)
        {
            _parentId = parentId;
            NewEntry = newEntry;
        }
    }
}