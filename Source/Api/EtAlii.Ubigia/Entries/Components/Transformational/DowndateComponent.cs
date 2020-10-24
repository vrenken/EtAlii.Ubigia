namespace EtAlii.Ubigia
{
    public class DowndateComponent : RelationComponent
    {
        protected internal override string GetName() => Name;
        private const string Name = "Downdate";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.DowndateComponent = this;
        }
    }
}