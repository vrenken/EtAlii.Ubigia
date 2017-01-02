namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;
    using System;

    [JsonObject(MemberSerialization.Fields)]
    public abstract class BlobPartBase : IBlobPart
    {
        public bool Stored { get { return _stored; } internal set { _stored = value; } }
        private bool _stored;

        protected internal abstract string Name { get; }

        public UInt64 Id { get { return _id; } set { _id = value; } }
        private UInt64 _id;
    }
}
