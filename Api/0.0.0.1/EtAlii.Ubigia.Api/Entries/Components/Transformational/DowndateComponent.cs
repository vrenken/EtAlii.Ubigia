namespace EtAlii.Ubigia.Api
{
    public class DowndateComponent : RelationComponent
    {
        protected internal override string Name => _name;
        private const string _name = "Downdate";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.DowndateComponent = this;
        }
    }
}
