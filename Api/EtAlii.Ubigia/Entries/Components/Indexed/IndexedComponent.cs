namespace EtAlii.Ubigia
{
    public class IndexedComponent : RelationComponent
    {
        protected internal override string GetName() => Name;
        private const string Name = "Indexed";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.IndexedComponent = this;
        }
    }
}
