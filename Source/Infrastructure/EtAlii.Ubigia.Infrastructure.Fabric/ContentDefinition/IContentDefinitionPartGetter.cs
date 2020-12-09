namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Threading.Tasks;

    public interface IContentDefinitionPartGetter
    {
        Task<ContentDefinitionPart> Get(Identifier identifier, ulong contentDefinitionPartId);
    }
}