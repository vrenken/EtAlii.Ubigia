namespace EtAlii.Ubigia
{
    public class ParentComponent : RelationComponent
    {
        protected internal override string Name => _name;
        private const string _name = "Parent";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.ParentComponent = this;
        }
    }
}
