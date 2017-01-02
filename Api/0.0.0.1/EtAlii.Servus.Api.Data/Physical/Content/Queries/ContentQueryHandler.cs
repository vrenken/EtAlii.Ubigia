namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;

    public class ContentQueryHandler : IContentQueryHandler
    {
        private readonly IDataConnection _connection;

        public ContentQueryHandler(IDataConnection connection)
        {
            _connection = connection;
        }

        public IReadOnlyContent Execute(ContentQuery query)
        {
            var content = _connection.Content.Retrieve(query.Identifier);
            return content;
        }
    }
}
