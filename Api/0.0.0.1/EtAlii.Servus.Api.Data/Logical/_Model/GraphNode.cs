namespace EtAlii.Servus.Api.Data
{
    public class GraphNode : GraphPathPart
    {
        public string Name { get; set; }

        public GraphNode(string name)
        {
            Name = name;
        }
    }
}