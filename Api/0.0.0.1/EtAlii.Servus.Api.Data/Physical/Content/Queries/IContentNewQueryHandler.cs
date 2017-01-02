namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;

    public interface IContentNewQueryHandler
    {
        IReadOnlyContent Execute(ContentNewQuery query);
    }
}
