namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;

    public interface ISpaceInitializer
    {
        void Initialize(Space space, SpaceTemplate template);
    }
}