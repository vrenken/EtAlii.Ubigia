namespace EtAlii.Ubigia.Api
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.Fields)]
    public abstract class CompositeComponent : ComponentBase
    {
        public ulong Id { get { return _id; } internal set { _id = value; } }
        private ulong _id;
    }
}
