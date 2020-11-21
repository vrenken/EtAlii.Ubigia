namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System.Threading.Tasks;

    internal partial class SignalRContentDataClient
    {
        public async Task Store(Identifier identifier, Content content)
        {
            await _invoker.Invoke(_contentConnection, SignalRHub.Content, "Post", identifier, content).ConfigureAwait(false);

            // TODO: Should this call be replaced by get instead? 
            BlobHelper.SetStored(content, true);
        }

        public async Task Store(Identifier identifier, ContentPart contentPart)
        {
            await _invoker.Invoke(_contentConnection, SignalRHub.Content, "PostPart", identifier, contentPart.Id, contentPart).ConfigureAwait(false);

            BlobPartHelper.SetStored(contentPart, true);
        }

        public async Task<IReadOnlyContent> Retrieve(Identifier identifier)
        {
            return await _invoker.Invoke<Content>(_contentConnection, SignalRHub.Content, "Get", identifier).ConfigureAwait(false);
        }

        public async Task<IReadOnlyContentPart> Retrieve(Identifier identifier, ulong contentPartId)
        {
            return await _invoker.Invoke<ContentPart>(_contentConnection, SignalRHub.Content, "GetPart", identifier, contentPartId).ConfigureAwait(false);
        }
    }
}
