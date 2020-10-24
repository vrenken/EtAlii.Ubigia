namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IContentDefinitionGetter
    {
        IReadOnlyContentDefinition Get(Identifier identifier);
    }
}