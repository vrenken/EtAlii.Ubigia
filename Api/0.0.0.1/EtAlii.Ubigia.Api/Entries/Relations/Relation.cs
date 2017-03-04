namespace EtAlii.Ubigia.Api
{
    using System;
    using System.Diagnostics;
    using Newtonsoft.Json;

    [DebuggerStepThrough]
    [JsonObject(MemberSerialization.Fields)]
    public partial struct Relation : IEquatable<Relation>
    {
        public Identifier Id => _id;
        private Identifier _id;

        public UInt64 Moment => _moment;
        private UInt64 _moment;

        public override string ToString()
        {
            return this == None ? "Relation.None" : String.Format("{0} (1)", _id, _moment);
        }

        public static readonly Relation None = new Relation
        {
            _id = Identifier.Empty,
            _moment = UInt64.MinValue,
        };
    }
}
