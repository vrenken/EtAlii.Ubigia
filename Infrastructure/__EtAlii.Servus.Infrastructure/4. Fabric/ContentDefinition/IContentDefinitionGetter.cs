namespace EtAlii.Servus.Infrastructure.Fabric
{
    using EtAlii.Servus.Api;

    public interface IContentDefinitionGetter
    {
        IReadOnlyContentDefinition Get(Identifier identifier);
    }
}