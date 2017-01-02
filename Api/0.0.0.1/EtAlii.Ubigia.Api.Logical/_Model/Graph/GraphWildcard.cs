namespace EtAlii.Ubigia.Api.Logical
{
    using System.Diagnostics;

    [DebuggerStepThrough]
    [DebuggerDisplay("{Pattern}")]
    public class GraphWildcard : GraphPathPart
    {
        public string Pattern { get; set; }

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