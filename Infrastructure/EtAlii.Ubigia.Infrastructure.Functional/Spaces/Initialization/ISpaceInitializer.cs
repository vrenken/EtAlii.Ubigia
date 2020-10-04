namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Threading.Tasks;

    public interface ISpaceInitializer
    {
        Task Initialize(Space space, SpaceTemplate template);
    }
}