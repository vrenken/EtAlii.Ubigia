namespace EtAlii.Servus.Api.Logical
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Fabric;

    public class ContentDefinitionQueryHandler : IContentDefinitionQueryHandler
    {
        private readonly IFabricContext _fabric;

        public ContentDefinitionQueryHandler(IFabricContext fabric)
        {
            _fabric = fabric;
        }


        public async Task<IReadOnlyContentDefinition> Execute(ContentDefinitionQuery query)
        {
            var contentDefinition = await _fabric.Content.RetrieveDefinition(query.Identifier);
            if (contentDefinition == null)
            {
                var newContentDefinition = new ContentDefinition
                {
                    Size = query.SizeInBytes,
                    TotalParts = query.RequiredParts,
                };
                await _fabric.Content.StoreDefinition(query.Identifier, newContentDefinition);
                contentDefinition = newContentDefinition;
            }

            return contentDefinition;
        }
    }
}
