namespace EtAlii.Servus.Api
{
    public class IndexesComponent : RelationsComponent  
    {
        protected internal override string Name { get { return _name; } }
        private const string _name = "Indexes";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.IndexesComponent.Add(Relations, markAsStored);
        }
    }
}
