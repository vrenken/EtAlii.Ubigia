namespace EtAlii.Servus.Api
{
    public class ParentComponent : RelationComponent
    {
        protected internal override string Name { get { return _name; } }
        private const string _name = "Parent";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.ParentComponent = this;
        }
    }
}
