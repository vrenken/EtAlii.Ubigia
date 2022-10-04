// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.Diagnostics;

    [DebuggerStepThrough]
    public partial struct Relation : IEquatable<Relation>, IBinarySerializable
    {
        public Identifier Id => _id;
        private Identifier _id;

        public ulong Moment => _moment;
        private ulong _moment;

        public override string ToString()
        {
            return this == None ? "Relation.None" : $"{Id} ({Moment})";
        }

        public static readonly Relation None = new()
        {
            _id = Identifier.Empty,
            _moment = ulong.MinValue,
        };
    }
}
