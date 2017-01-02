namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GraphPathBuilder : IGraphPathBuilder
    {
        private readonly List<GraphPathPart> _parts = new List<GraphPathPart>();

        public IGraphPathBuilder Add(string nodeName)
        {
            _parts.Add(new GraphNode(nodeName));
            return this;
        }

        public IGraphPathBuilder Add(GraphRelation relation)
        {
            _parts.Add(relation);
            return this;
        }

        public GraphPath ToPath()
        {
            return new GraphPath(_parts.ToArray());
        }

        public void Clear()
        {
            _parts.Clear();
        }
    }
}
