namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IContentPartGetter
    {
        IReadOnlyContentPart Get(Identifier identifier, ulong contentPartId);
    }
}