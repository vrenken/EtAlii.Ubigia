namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System.Threading.Tasks;

    public interface ILogicalContentDefinitionSet
    {
        Task<IReadOnlyContentDefinition> Get(Identifier identifier);
        Task<IReadOnlyContentDefinitionPart> Get(Identifier identifier, ulong contentDefinitionPartId);
        void Store(in Identifier identifier, ContentDefinitionPart contentDefinitionPart);
        void Store(in Identifier identifier, ContentDefinition contentDefinition);
    }
}