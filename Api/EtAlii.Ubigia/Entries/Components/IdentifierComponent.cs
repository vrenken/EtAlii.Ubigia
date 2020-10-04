namespace EtAlii.Ubigia
{
    public class IdentifierComponent : NonCompositeComponent
    {
        internal IdentifierComponent()
        { 
        }

        public Identifier Id { get; internal set; }

        protected internal override string GetName() => Name;
        private const string Name = "Identifier";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.IdComponent = this;
        }
    }
}
