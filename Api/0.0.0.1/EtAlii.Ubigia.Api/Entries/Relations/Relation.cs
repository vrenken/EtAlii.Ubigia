namespace EtAlii.Ubigia.Api
{
    using System;
    using System.Diagnostics;
    using Newtonsoft.Json;

    [DebuggerStepThrough]
    [JsonObject(MemberSerialization.Fields)]
    public partial struct Relation : IEquatable<Relation>
    {
        public Identifier Id { get; private set; }

        public UInt64 Moment { get; private set; }

        public override string ToString()
        {
            return this == None ? "Relation.None" : String.Format("{0} (1)", Id, Moment);
        }

        public static readonly Relation None = new Relation
        {
            Id = Identifier.Empty,
            Moment = UInt64.MinValue,
        };
    }
}
