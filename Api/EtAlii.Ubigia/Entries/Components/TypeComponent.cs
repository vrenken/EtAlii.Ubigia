namespace EtAlii.Ubigia
{
    public class TypeComponent : NonCompositeComponent
    {
        internal TypeComponent()
        {
        }

        public string Type { get; internal set; }

        protected internal override string GetName() => Name;
        private const string Name = "Type";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.TypeComponent = this;
        }
    }
}
