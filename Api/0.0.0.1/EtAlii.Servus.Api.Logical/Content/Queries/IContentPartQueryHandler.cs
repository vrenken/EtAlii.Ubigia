namespace EtAlii.Servus.Api.Logical
{
    using System.Threading.Tasks;

    public interface IContentPartQueryHandler
    {
        Task Execute(ContentPartQuery query);
    }
}
