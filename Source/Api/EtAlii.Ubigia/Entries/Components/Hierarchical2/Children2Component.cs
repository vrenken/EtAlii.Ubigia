namespace EtAlii.Ubigia
{
    public class Children2Component : RelationsComponent  
    {
        protected internal override string GetName() => Name;
        private const string Name = "Children2";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.Children2Component.Add(Relations, markAsStored);
        }
    }
}
