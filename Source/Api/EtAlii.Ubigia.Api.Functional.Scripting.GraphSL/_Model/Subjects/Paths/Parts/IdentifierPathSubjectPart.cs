namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    public class IdentifierPathSubjectPart : PathSubjectPart
    {
        public Identifier Identifier { get; }

        public IdentifierPathSubjectPart(in Identifier identifier)
        {
            Identifier = identifier;
        }

        public override string ToString()
        {
            return $"&{Identifier}";
        }
    }
}
