namespace EtAlii.Ubigia.Api.Functional
{
    public class IdentifierPathSubjectPart : PathSubjectPart
    {
        public Identifier Identifier { get; }

        public IdentifierPathSubjectPart(Identifier identifier)
        {
            Identifier = identifier;
        }

        public override string ToString()
        {
            return $"&{Identifier}";
        }
    }
}
