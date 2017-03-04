namespace EtAlii.Ubigia.Api
{
    public class NextComponent : RelationComponent
    {
        protected internal override string Name => _name;
        private const string _name = "Next";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.NextComponent = this;
        }
    }
}
