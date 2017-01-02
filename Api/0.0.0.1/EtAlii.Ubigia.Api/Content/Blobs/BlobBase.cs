namespace EtAlii.Ubigia.Api
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.Fields)]
    public abstract class BlobBase : IBlob
    {
        public bool Stored { get { return _stored; } internal set { _stored = value; } }
        private bool _stored;

        protected internal abstract string Name { get; }

        public BlobSummary Summary { get { return _summary; } internal set { _summary = value; } }
        private BlobSummary _summary;

        public UInt64 TotalParts { get { return _totalParts; } set { _totalParts = value; } }
        private UInt64 _totalParts;

        protected BlobBase()
        {
        }
    }
}
