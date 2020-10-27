namespace EtAlii.Ubigia
{
    public class Parent2Component : RelationComponent
    {
        protected internal override string GetName() => Name;
        private const string Name = "Parent2";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.Parent2Component = this;
        }
    }
}
