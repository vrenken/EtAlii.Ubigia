namespace EtAlii.Ubigia.Infrastructure
{
    public interface IComponentManager
    {
        // This object is ugly, but right now the only way to move this interface into EtAlii.Ubigia.Infrastructure.Transport.
        void Start(object iAppBuilder);
        void Stop();
    }
}