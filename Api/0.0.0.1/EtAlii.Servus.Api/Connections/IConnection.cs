namespace EtAlii.Servus.Api
{

    public interface IConnection 
    {
        bool IsConnected  { get; }
        void Close();

        void Open(string address, string accountName, string password);
    }
}
