namespace EtAlii.Ubigia.Api.Logical
{
    public interface IHierarchicalRelationDuplicator
    {
        void Duplicate(IReadOnlyEntry source, IEditableEntry target);
        void Duplicate(IReadOnlyEntry source, IEditableEntry target, Identifier relationToExclude);
    }
}