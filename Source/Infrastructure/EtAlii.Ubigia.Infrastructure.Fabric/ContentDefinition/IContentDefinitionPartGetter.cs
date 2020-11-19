namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Threading.Tasks;

    public interface IContentDefinitionPartGetter
    {
        Task<IReadOnlyContentDefinitionPart> Get(Identifier identifier, ulong contentDefinitionPartId);
    }
}