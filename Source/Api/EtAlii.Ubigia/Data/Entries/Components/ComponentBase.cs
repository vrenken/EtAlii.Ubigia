namespace EtAlii.Ubigia
{
    public abstract class ComponentBase : IComponent
    {
        public bool Stored { get; internal set; }

        protected internal abstract string Name { get; }

        protected internal abstract void Apply(IComponentEditableEntry entry, bool markAsStored);
    }
}
 