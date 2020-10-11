namespace EtAlii.xTechnology.Hosting
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface IHostTestContext
    {
        Task Start(PortRange portRange);

        Task Stop();

        HttpMessageHandler CreateHandler();
    }
}