namespace EtAlii.Ubigia
{
    public class NextComponent : RelationComponent
    {
        protected internal override string GetName() => Name;
        private const string Name = "Next";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.NextComponent = this;
        }
    }
}
