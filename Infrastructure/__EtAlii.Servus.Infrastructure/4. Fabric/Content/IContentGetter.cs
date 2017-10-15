namespace EtAlii.Servus.Infrastructure.Fabric
{
    using EtAlii.Servus.Api;

    public interface IContentGetter
    {
        IReadOnlyContent Get(Identifier identifier);
    }
}