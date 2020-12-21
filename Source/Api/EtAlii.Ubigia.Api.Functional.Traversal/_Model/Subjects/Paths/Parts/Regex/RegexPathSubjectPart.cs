namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public class RegexPathSubjectPart : PathSubjectPart
    {
        public string Regex { get; }

        public RegexPathSubjectPart(string regex)
        {
            Regex = regex;
        }

        public override string ToString()
        {
            return Regex;
        }
    }
}
