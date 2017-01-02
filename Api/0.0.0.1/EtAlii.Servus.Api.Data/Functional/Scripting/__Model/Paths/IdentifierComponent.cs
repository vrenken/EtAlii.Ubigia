namespace EtAlii.Servus.Api.Data
{
    using System;

    public class IdentifierComponent : PathComponent
    {
        public Identifier Id { get; private set; }

        public IdentifierComponent(Identifier id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Id.ToDotSeparatedString();
        }
    }
}
