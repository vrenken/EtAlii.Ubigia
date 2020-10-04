namespace EtAlii.Ubigia
{
    public class IndexesComponent : RelationsComponent  
    {
        protected internal override string GetName() => Name;
        private const string Name = "Indexes";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.IndexesComponent.Add(Relations, markAsStored);
        }
    }
}
