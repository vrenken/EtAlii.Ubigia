namespace EtAlii.Servus.Api.Logical
{
    using System.Diagnostics;

    [DebuggerDisplay("{Root}")]
    public class GraphRootStartNode : GraphPathPart
    {
        public string Root { get; private set; }

        public GraphRootStartNode(string root)
        {
            Root = root;
        }

        public override string ToString()
        {
            return Root;
        }
    }
}