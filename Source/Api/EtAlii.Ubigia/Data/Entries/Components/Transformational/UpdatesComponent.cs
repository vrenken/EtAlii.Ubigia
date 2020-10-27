namespace EtAlii.Ubigia
{
    public class UpdatesComponent : RelationsComponent 
    {
        protected internal override string GetName() => Name;
        private const string Name = "Updates";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.UpdatesComponent.Add(Relations, markAsStored);
        }
    }
}
