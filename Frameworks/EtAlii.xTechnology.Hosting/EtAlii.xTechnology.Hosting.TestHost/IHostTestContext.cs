namespace EtAlii.xTechnology.Hosting
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface IHostTestContext
    {
        Task Start();

        Task Stop();

        HttpMessageHandler CreateHandler();
    }
}