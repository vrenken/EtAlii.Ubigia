namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Threading.Tasks;

    public interface IContentDefinitionSet
    {
        Task<ContentDefinition> Get(Identifier identifier);
        Task<ContentDefinitionPart> Get(Identifier identifier, ulong contentDefinitionPartId);
        void Store(in Identifier identifier, ContentDefinitionPart contentDefinitionPart);
        void Store(in Identifier identifier, ContentDefinition contentDefinition);
    }
}