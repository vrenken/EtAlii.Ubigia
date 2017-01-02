namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;

    public class ContentNewQueryHandler : IContentNewQueryHandler
    {
        private readonly IDataConnection _connection;

        public ContentNewQueryHandler(IDataConnection connection)
        {
            _connection = connection;
        }

        public IReadOnlyContent Execute(ContentNewQuery query)
        {
            var content = _connection.Content.Retrieve(query.Identifier);
            if (content == null)
            {
                var newContent = new EtAlii.Servus.Api.Content
                {
                    TotalParts = query.RequiredParts,
                };
                _connection.Content.Store(query.Identifier, newContent);
                content = newContent;
            }
            return content;
        }

        public IReadOnlyContent Execute(Identifier identifier)
        {
            var content = _connection.Content.Retrieve(identifier);
            return content;
        }
    }
}
