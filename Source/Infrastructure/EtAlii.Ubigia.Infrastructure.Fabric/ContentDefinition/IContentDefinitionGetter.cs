namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Threading.Tasks;

    public interface IContentDefinitionGetter
    {
        Task<IReadOnlyContentDefinition> Get(Identifier identifier);
    }
}