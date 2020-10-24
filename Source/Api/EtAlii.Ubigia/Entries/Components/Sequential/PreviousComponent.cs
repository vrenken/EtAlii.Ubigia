namespace EtAlii.Ubigia
{
    public class PreviousComponent : RelationComponent
    {
        protected internal override string GetName() => Name;
        private const string Name = "Previous";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.PreviousComponent = this;
        }
    }
}
