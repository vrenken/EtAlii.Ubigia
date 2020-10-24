namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Threading.Tasks;

    public interface IEntryCacheStoreHandler
    {
        Task Handle(Identifier identifier);
    }
}