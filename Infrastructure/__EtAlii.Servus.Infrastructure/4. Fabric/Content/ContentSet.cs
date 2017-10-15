namespace EtAlii.Servus.Infrastructure.Fabric
{
    using EtAlii.Servus.Api;

    public class ContentSet : IContentSet
    {
        private readonly IContentGetter _contentGetter;
        private readonly IContentPartGetter _contentPartGetter;
        private readonly IContentStorer _contentStorer;
        private readonly IContentPartStorer _contentPartStorer;

        public ContentSet(
            IContentGetter contentGetter, 
            IContentPartGetter contentPartGetter, 
            IContentStorer contentStorer, 
            IContentPartStorer contentPartStorer)
        {
            _contentGetter = contentGetter;
            _contentPartGetter = contentPartGetter;
            _contentStorer = contentStorer;
            _contentPartStorer = contentPartStorer;
        }


        public IReadOnlyContent Get(Identifier identifier)
        {
            return _contentGetter.Get(identifier);
        }

        public IReadOnlyContentPart Get(Identifier identifier, ulong contentPartId)
        {
            return _contentPartGetter.Get(identifier, contentPartId);
        }

        public void Store(Identifier identifier, ContentPart contentPart)
        {
            _contentPartStorer.Store(identifier, contentPart);
        }

        public void Store(Identifier identifier, Content content)
        {
            _contentStorer.Store(identifier, content);
        }
    }
}