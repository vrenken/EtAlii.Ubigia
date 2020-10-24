namespace EtAlii.Ubigia.Provisioning
{
    using System.Threading.Tasks;

    public interface IUpdater
    {
        Task Start();
        Task Stop();
    }
}