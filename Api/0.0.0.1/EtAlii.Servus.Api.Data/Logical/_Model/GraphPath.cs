namespace EtAlii.Servus.Api.Data
{
    using System.Collections;
    using System.Collections.Generic;

    public class GraphPath
    {
        public GraphPathPart[] Parts { get { return _parts; } }
        private readonly GraphPathPart[] _parts;

        public GraphPath(GraphPathPart[] parts)
        {
            _parts = parts;
        }
    }
}