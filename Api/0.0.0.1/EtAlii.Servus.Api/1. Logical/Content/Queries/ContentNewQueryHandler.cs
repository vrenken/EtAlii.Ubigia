namespace EtAlii.Servus.Api.Logical
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Fabric;

    public class ContentNewQueryHandler : IContentNewQueryHandler
    {
        private readonly IFabricContext _fabric;

        public ContentNewQueryHandler(IFabricContext fabric)
        {
            _fabric = fabric;
        }

        public async Task<IReadOnlyContent> Execute(ContentNewQuery query)
        {
            var content = await _fabric.Content.Retrieve(query.Identifier);
            if (content == null)
            {
                var newContent = new Content
                {
                    TotalParts = query.RequiredParts,
                };
                await _fabric.Content.Store(query.Identifier, newContent);
                content = newContent;
            }
            return content;
        }

        public async Task<IReadOnlyContent> Execute(Identifier identifier)
        {
            var content = await _fabric.Content.Retrieve(identifier);
            return content;
        }
    }
}
