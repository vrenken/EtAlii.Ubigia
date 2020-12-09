namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Threading.Tasks;

    public interface IContentDefinitionRepository
    {
        void Store(in Identifier identifier, ContentDefinition contentDefinition);
        Task Store(Identifier identifier, ContentDefinitionPart contentDefinitionPart);
        Task<ContentDefinition> Get(Identifier identifier);
    }
}