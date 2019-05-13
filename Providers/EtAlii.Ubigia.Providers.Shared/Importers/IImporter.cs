namespace EtAlii.Ubigia.Provisioning
{
    using System.Threading.Tasks;

    public interface IImporter
    {
        Task Start();
        Task Stop();
    }
}