namespace EtAlii.Servus.Api.Logical
{
    using System.Threading.Tasks;

    public interface IContentPartStoreCommandHandler
    {
        Task Execute(ContentPartStoreCommand command);
    }
}
