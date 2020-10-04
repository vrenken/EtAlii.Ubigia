namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IContentDefinitionPartGetter
    {
        IReadOnlyContentDefinitionPart Get(Identifier identifier, ulong contentDefinitionPartId);
    }
}