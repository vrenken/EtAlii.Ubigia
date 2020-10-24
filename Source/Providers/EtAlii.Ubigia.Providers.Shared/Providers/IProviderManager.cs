namespace EtAlii.Ubigia.Provisioning
{
    using System.Threading.Tasks;

    public interface IProviderManager
    {
        string Status { get; }
        Task Start();
        Task Stop();
    }
}