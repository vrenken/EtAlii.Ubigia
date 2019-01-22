namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public sealed class AccountNotificationClientStub : IAccountNotificationClient 
    {
        public async Task Connect(IStorageConnection connection)
        {
            await Task.CompletedTask;
        }

        public async Task Disconnect(IStorageConnection connection)
        {
            await Task.CompletedTask;
        }
    }
}
