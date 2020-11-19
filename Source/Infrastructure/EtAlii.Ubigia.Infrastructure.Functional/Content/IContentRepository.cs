namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IContentRepository
    {
        Task Store(Identifier identifier, Content content, IEnumerable<ContentPart> contentParts);
        Task Store(Identifier identifier, Content content);
        Task Store(Identifier identifier, ContentPart contentPart);
        Task<IReadOnlyContent> Get(Identifier identifier);
        Task<IReadOnlyContentPart> Get(Identifier identifier, ulong contentPartId);
    }
}