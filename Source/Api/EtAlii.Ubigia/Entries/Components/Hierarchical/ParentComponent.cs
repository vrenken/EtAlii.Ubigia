namespace EtAlii.Ubigia
{
    public class ParentComponent : RelationComponent
    {
        protected internal override string GetName() => Name;
        private const string Name = "Parent";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.ParentComponent = this;
        }
    }
}
