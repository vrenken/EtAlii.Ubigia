namespace EtAlii.Servus.Api.Logical
{
    using System.Diagnostics;

    [DebuggerStepThrough]
    [DebuggerDisplay("{Limit}")]
    public class GraphTraversingWildcard : GraphPathPart
    {
        public int Limit { get; set; }

        public GraphTraversingWildcard(int limit)
        {
            Limit = limit;
        }

        public override string ToString()
        {
            return Limit == 0 ? "**" : $"*{Limit}*";
        }
    }
}