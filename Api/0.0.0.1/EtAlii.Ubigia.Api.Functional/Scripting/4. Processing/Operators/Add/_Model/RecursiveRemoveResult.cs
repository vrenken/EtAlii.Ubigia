namespace EtAlii.Ubigia.Api.Functional
{
    internal class RecursiveRemoveResult
    {
        public readonly Identifier ParentId;
        public readonly IEditableEntry NewEntry;

        public RecursiveRemoveResult(Identifier parentId, IEditableEntry newEntry)
        {
            ParentId = parentId;
            NewEntry = newEntry;
        }
    }
}