namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;

    public class ContentDataClientStub : IContentDataClient 
    {
        public void StoreDefinition(Identifier identifier, ContentDefinition contentDefinition)
        {
        }

        public void StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
        }

        public IReadOnlyContentDefinition RetrieveDefinition(Identifier identifier)
        {
            return null;
        }

        public void Store(Identifier identifier, EtAlii.Servus.Api.Content content)
        {
        }

        public void Store(Identifier identifier, ContentPart contentPart)
        {
        }

        public IReadOnlyContent Retrieve(Identifier identifier)
        {
            return null;
        }

        public IReadOnlyContentPart Retrieve(Identifier identifier, ulong contentPartId)
        {
            return null;
        }

        public void Connect()
        {
        }

        public void Disconnect()
        {
        }
    }
}
