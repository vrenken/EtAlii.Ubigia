namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;

    internal partial class SystemContentDataClient
    {
        public Task Store(Identifier identifier, Content content)
        {
            _infrastructure.Content.Store(identifier, content);
            BlobHelper.SetStored(content, true);

            return Task.CompletedTask;
        }

        public Task Store(Identifier identifier, ContentPart contentPart)
        {
            _infrastructure.Content.Store(identifier, contentPart);
            BlobPartHelper.SetStored(contentPart, true);

            return Task.CompletedTask;
        }

        public Task<IReadOnlyContent> Retrieve(Identifier identifier)
        {
            var result = _infrastructure.Content.Get(identifier);
            return Task.FromResult(result);
        }

        public Task<IReadOnlyContentPart> Retrieve(Identifier identifier, ulong contentPartId)
        {
            var result = _infrastructure.Content.Get(identifier, contentPartId);
            return Task.FromResult(result);
        }
    }
}
