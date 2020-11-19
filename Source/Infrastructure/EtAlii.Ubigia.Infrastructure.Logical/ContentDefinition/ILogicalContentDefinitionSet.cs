namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System.Threading.Tasks;

    public interface ILogicalContentDefinitionSet
    {
        Task<IReadOnlyContentDefinition> Get(Identifier identifier);
        Task<IReadOnlyContentDefinitionPart> Get(Identifier identifier, ulong contentDefinitionPartId);
        void Store(Identifier identifier, ContentDefinitionPart contentDefinitionPart);
        void Store(Identifier identifier, ContentDefinition contentDefinition);
    }
}