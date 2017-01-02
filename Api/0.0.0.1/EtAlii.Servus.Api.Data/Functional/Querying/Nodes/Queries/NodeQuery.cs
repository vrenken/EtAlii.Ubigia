namespace EtAlii.Servus.Api.Data
{
    public class NodeQuery
    {
        private readonly IDataConnection _connection;

        internal NodeQuery(IDataConnection connection)
        {
            _connection = connection;
        }

        public Node Execute(Identifier id)
        {
            return null;
        }

        public Node Get(Node node)
        {
            return null;
        }
    }
}
