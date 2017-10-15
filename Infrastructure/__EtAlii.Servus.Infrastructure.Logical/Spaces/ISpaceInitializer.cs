namespace EtAlii.Servus.Infrastructure.Logical
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;

    public interface ISpaceInitializer
    {
        void Initialize(Space space, SpaceTemplate template);
    }
}