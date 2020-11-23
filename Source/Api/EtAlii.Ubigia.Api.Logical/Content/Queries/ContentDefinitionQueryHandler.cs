namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    public class ContentDefinitionQueryHandler : IContentDefinitionQueryHandler
    {
        private readonly IFabricContext _fabric;

        public ContentDefinitionQueryHandler(IFabricContext fabric)
        {
            _fabric = fabric;
        }


        public async Task<IReadOnlyContentDefinition> Execute(ContentDefinitionQuery query)
        {
            var contentDefinition = await _fabric.Content.RetrieveDefinition(query.Identifier).ConfigureAwait(false);
            if (contentDefinition == null)
            {
                var newContentDefinition = new ContentDefinition
                {
                    Size = query.SizeInBytes,
                    TotalParts = query.RequiredParts,
                };
                await _fabric.Content.StoreDefinition(query.Identifier, newContentDefinition).ConfigureAwait(false);
                contentDefinition = newContentDefinition;
            }

            return contentDefinition;
        }
    }
}
