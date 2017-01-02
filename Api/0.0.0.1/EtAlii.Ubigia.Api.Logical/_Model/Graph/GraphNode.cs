namespace EtAlii.Ubigia.Api.Logical
{
    using System.Diagnostics;

    [DebuggerStepThrough]
    [DebuggerDisplay("{Name}")]
    public class GraphNode : GraphPathPart
    {
        public string Name { get; set; }

        public GraphNode(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}