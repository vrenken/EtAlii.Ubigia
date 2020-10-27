namespace EtAlii.Ubigia
{
    public class TagComponent : NonCompositeComponent
    {
        internal TagComponent()
        {
        }

        public string Tag { get; internal set; }

        protected internal override string GetName() => Name;
        private const string Name = "Tag";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.TagComponent = this;
        }
    }
}
