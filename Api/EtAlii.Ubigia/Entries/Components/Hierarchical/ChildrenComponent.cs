namespace EtAlii.Ubigia
{
    public class ChildrenComponent : RelationsComponent 
    {
        protected internal override string GetName() => Name;
        private const string Name = "Children";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.ChildrenComponent.Add(Relations, markAsStored);
        }
    }
}
