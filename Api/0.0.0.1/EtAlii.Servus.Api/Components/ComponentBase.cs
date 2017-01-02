namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.Fields)]
    public abstract class ComponentBase : IComponent
    {
        public bool Stored { get { return _stored; } internal set { _stored = value; } }
        private bool _stored;

        protected ComponentBase()
        {
        }

        protected internal abstract string Name { get; }

        protected internal abstract void Apply(IComponentEditableEntry entry, bool markAsStored);
    }
}
