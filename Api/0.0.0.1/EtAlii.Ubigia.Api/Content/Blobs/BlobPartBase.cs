namespace EtAlii.Ubigia.Api
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.Fields)]
    public abstract class BlobPartBase : IBlobPart
    {
        public bool Stored { get; internal set; }

        protected internal abstract string Name { get; }

        public UInt64 Id { get; set; }
    }
}
