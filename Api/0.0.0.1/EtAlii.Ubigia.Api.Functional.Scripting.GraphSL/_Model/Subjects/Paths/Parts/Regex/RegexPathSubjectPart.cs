namespace EtAlii.Ubigia.Api.Functional
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
