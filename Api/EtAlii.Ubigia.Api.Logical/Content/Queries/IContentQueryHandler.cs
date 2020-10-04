namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public interface IContentQueryHandler
    {
        Task<IReadOnlyContent> Execute(ContentQuery query);
    }
}
