namespace EtAlii.Ubigia.Api.Logical
{
    using System.Diagnostics;

    [DebuggerStepThrough]
    [DebuggerDisplay("{" + nameof(Pattern) + "}")]
    public class GraphWildcard : GraphPathPart
    {
        public readonly string Pattern;

        public GraphWildcard(string pattern)
        {
            Pattern = pattern;
        }

        public override string ToString()
        {
            return Pattern;
        }
    }
}