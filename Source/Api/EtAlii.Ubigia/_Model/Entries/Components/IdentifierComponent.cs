// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System.IO;

    public sealed class IdentifierComponent : NonCompositeComponent
    {
        internal IdentifierComponent()
        {
        }

        public Identifier Id { get; internal set; }

        /// <inheritdoc />
        protected internal override string Name => "Identifier";

        /// <inheritdoc />
        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.IdComponent = this;
        }


        public override void Write(BinaryWriter writer)
        {
            writer.Write(Id);
        }

        public override void Read(BinaryReader reader)
        {
            Id = reader.Read<Identifier>();
        }
    }
}
