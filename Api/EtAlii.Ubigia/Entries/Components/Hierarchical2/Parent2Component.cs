namespace EtAlii.Ubigia
{
    public class Parent2Component : RelationComponent
    {
        protected internal override string Name => _name;
        private const string _name = "Parent2";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.Parent2Component = this;
        }
    }
}
