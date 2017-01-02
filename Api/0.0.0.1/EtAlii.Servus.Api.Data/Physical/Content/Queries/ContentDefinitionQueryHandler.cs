namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;

    public class ContentDefinitionQueryHandler : IContentDefinitionQueryHandler
    {
        private readonly IDataConnection _connection;

        public ContentDefinitionQueryHandler(IDataConnection connection)
        {
            _connection = connection;
        }


        public IReadOnlyContentDefinition Execute(ContentDefinitionQuery query)
        {
            var contentDefinition = _connection.Content.RetrieveDefinition(query.Identifier);
            if (contentDefinition == null)
            {
                var newContentDefinition = new ContentDefinition
                {
                    Size = query.SizeInBytes,
                    TotalParts = query.RequiredParts,
                };
                _connection.Content.StoreDefinition(query.Identifier, newContentDefinition);
                contentDefinition = newContentDefinition;
            }

            return contentDefinition;
        }
    }
}
