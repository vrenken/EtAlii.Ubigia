namespace EtAlii.Servus.Api.Functional
{
    public class RegexPathSubjectPart : PathSubjectPart
    {
        public string Regex { get; private set; }

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
