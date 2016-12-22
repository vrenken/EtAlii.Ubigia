namespace EtAlii.Servus.Api.Functional
{
    public class WildcardPathSubjectPart : PathSubjectPart
    {
        public string Pattern { get; private set; }

        public WildcardPathSubjectPart(string pattern)
        {
            Pattern = pattern;
        }

        public override string ToString()
        {
            return Pattern;
        }
    }
}
