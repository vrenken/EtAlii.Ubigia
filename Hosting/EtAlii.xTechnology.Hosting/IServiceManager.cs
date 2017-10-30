namespace EtAlii.xTechnology.Hosting
{
    public interface IServiceManager
    {
        void Initialize(IHostService[] services);
        void Start();
        void Stop();
    }
}
