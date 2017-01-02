namespace EtAlii.Servus.Api.Transport
{
    public class DataConnectionDetails
    {
        public Storage Storage { get; private set; }
        public Account Account { get; private set; }
        public Space Space { get; private set; }

        public DataConnectionDetails(
            Storage storage, 
            Account account, 
            Space space)
        {
            Storage = storage;
            Account = account;
            Space = space;
        }
    }
}
