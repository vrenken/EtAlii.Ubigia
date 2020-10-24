namespace EtAlii.Ubigia.Provisioning
{
    using System.Threading.Tasks;

    public interface IProvider
    {
        IProviderConfiguration Configuration { get; }
        Task Stop();
        Task Start();
    }
}
