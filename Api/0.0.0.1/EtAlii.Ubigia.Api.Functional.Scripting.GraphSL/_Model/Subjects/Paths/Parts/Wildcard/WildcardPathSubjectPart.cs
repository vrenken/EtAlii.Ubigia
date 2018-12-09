namespace EtAlii.Ubigia.Api.Functional
{
    public class WildcardPathSubjectPart : PathSubjectPart
    {
        public string Pattern { get; }

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
