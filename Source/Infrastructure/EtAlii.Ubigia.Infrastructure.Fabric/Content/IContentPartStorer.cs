namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Threading.Tasks;

    public interface IContentPartStorer
    {
        Task Store(Identifier identifier, ContentPart contentPart);
    }
}