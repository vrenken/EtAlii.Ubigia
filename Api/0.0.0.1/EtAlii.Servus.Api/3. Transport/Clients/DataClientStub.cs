namespace EtAlii.Servus.Api.Transport
{
    using System.Threading.Tasks;

    public class DataClientStub : IDataClient
    {
        public async Task Connect(ITransport transport)
        {
            await Task.Run(() => { });
        }

        public async Task Disconnect(ITransport transport)
        {
            await Task.Run(() => { });
        }
    }
}
