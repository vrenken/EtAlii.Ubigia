namespace EtAlii.Ubigia.Api
{
    public class PreviousComponent : RelationComponent
    {
        protected internal override string Name { get { return _name; } }
        private const string _name = "Previous";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.PreviousComponent = this;
        }
    }
}
