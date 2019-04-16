namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;

    public interface ISpaceInitializer
    {
        Task Initialize(Space space, SpaceTemplate template);
    }
}