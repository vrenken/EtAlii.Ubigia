namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    public class ContentNewQueryHandler : IContentNewQueryHandler
    {
        private readonly IFabricContext _fabric;

        public ContentNewQueryHandler(IFabricContext fabric)
        {
            _fabric = fabric;
        }

        public async Task<IReadOnlyContent> Execute(ContentNewQuery query)
        {
            var content = await _fabric.Content.Retrieve(query.Identifier).ConfigureAwait(false);
            if (content == null)
            {
                var newContent = new Content
                {
                    TotalParts = query.RequiredParts,
                };
                await _fabric.Content.Store(query.Identifier, newContent).ConfigureAwait(false);
                content = newContent;
            }
            return content;
        }

        public async Task<IReadOnlyContent> Execute(Identifier identifier)
        {
            var content = await _fabric.Content.Retrieve(identifier).ConfigureAwait(false);
            return content;
        }
    }
}
