namespace EtAlii.Servus.Api
{
    public class Parent2Component : RelationComponent
    {
        protected internal override string Name { get { return _name; } }
        private const string _name = "Parent2";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.Parent2Component = this;
        }
    }
}
