// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Threading.Tasks;

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


        public Task<Content> Get(Identifier identifier)
        {
            return _contentGetter.Get(identifier);
        }

        public Task<ContentPart> Get(Identifier identifier, ulong contentPartId)
        {
            return _contentPartGetter.Get(identifier, contentPartId);
        }

        public void Store(in Identifier identifier, ContentPart contentPart)
        {
            _contentPartStorer.Store(identifier, contentPart);
        }

        public void Store(in Identifier identifier, Content content)
        {
            _contentStorer.Store(identifier, content);
        }
    }
}