namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public class IdentifierPathSubjectPart : PathSubjectPart
    {
        public Identifier Identifier { get; private set; }

        public IdentifierPathSubjectPart(Identifier identifier)
        {
            Identifier = identifier;
        }

        public override string ToString()
        {
            return String.Format("&{0}", Identifier.ToString());
        }
    }
}
