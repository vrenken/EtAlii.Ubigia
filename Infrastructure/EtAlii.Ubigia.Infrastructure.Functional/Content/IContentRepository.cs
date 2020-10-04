namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Collections.Generic;

    public interface IContentRepository
    {
        void Store(Identifier identifier, Content content, IEnumerable<ContentPart> contentParts);
        void Store(Identifier identifier, Content content);
        void Store(Identifier identifier, ContentPart contentPart);
        IReadOnlyContent Get(Identifier identifier);
        IReadOnlyContentPart Get(Identifier identifier, ulong contentPartId);
    }
}