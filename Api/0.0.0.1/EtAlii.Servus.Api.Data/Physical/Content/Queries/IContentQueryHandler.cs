namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;

    public interface IContentQueryHandler
    {
        IReadOnlyContent Execute(ContentQuery query);
    }
}
