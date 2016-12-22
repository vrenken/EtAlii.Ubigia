namespace EtAlii.Servus.Api
{
    public class DowndateComponent : RelationComponent
    {
        protected internal override string Name { get { return _name; } }
        private const string _name = "Downdate";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.DowndateComponent = this;
        }
    }
}
