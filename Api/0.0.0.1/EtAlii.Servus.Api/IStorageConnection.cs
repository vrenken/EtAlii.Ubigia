namespace EtAlii.Servus.Api
{

    public interface IStorageConnection 
    {
        Storage Storage { get; }
        bool IsConnected  { get; }
        void Close();

        void Open(string address, string accountName);
        void Open(string address, string accountName, string password);
    }
}
