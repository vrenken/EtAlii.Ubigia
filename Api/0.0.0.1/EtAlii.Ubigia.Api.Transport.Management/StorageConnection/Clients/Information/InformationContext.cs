namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System.Threading.Tasks;

    public class InformationContext : IInformationContext
    {
        public IInformationDataClient Data { get; }

        public InformationContext(IInformationDataClient data)
        {
            Data = data;
        }

        public async Task Open(IStorageConnection storageConnection)
        {
            await Data.Connect(storageConnection);
        }

        public async Task Close(IStorageConnection storageConnection)
        {
            await Data.Disconnect(storageConnection);
        }

    }

}