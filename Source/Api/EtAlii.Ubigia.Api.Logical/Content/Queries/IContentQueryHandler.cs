namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public interface IContentQueryHandler
    {
        Task<Content> Execute(ContentQuery query);
    }
}
