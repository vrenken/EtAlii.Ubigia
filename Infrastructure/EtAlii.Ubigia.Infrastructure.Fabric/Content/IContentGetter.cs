namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.Ubigia.Api;

    public interface IContentGetter
    {
        IReadOnlyContent Get(Identifier identifier);
    }
}