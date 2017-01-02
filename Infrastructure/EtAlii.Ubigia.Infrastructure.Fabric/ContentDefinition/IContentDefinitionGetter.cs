namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.Ubigia.Api;

    public interface IContentDefinitionGetter
    {
        IReadOnlyContentDefinition Get(Identifier identifier);
    }
}