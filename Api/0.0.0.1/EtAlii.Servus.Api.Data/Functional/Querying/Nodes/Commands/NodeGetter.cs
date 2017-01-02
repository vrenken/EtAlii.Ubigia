namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Linq;

    public class NodeGetter
    {
        private readonly IDataConnection _connection;

        internal NodeGetter(IDataConnection connection)
        {
            _connection = connection;
        }

        public Node Get(Identifier id)
        {
            return null;
        }

        public Node Get(Node node)
        {
            return null;
        }
    }
}
