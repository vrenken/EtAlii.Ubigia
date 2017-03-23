namespace EtAlii.Ubigia.Api
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.Fields)]
    public abstract class BlobBase : IBlob
    {
        public bool Stored { get; internal set; }

        protected internal abstract string Name { get; }

        public BlobSummary Summary { get; internal set; }

        public UInt64 TotalParts { get; set; }
    }
}
