namespace EtAlii.Servus.Api.Logical
{
    using System.Threading.Tasks;

    public interface IContentNewQueryHandler
    {
        Task<IReadOnlyContent> Execute(ContentNewQuery query);
    }
}
