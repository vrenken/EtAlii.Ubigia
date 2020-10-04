namespace EtAlii.Ubigia
{
    using System;
    using System.Diagnostics;

    [DebuggerStepThrough]
    public partial struct Relation : IEquatable<Relation>
    {
        public Identifier Id { get; private set; }

        public ulong Moment { get; private set; }

        public override string ToString()
        {
            return this == None ? "Relation.None" : $"{Id} ({Moment})";
        }

        public static readonly Relation None = new Relation
        {
            Id = Identifier.Empty,
            Moment = ulong.MinValue,
        };
    }
}
