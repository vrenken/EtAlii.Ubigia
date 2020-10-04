namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IContentGetter
    {
        IReadOnlyContent Get(Identifier identifier);
    }
}