namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Threading.Tasks;

    public class DataConnectionStub : IDataConnection
    {
        public Storage Storage { get; }

        public Account Account { get; }

        public Space Space { get; }

        public IEntryContext Entries { get; }

        public IRootContext Roots { get; }

        public IContentContext Content { get; }

        public IPropertiesContext Properties { get; }

        public bool IsConnected { get; } = false;

        public IDataConnectionConfiguration Configuration { get; }

        public async Task Open()
        {
            await Task.CompletedTask;
        }
        public async Task Close()
        {
            await Task.CompletedTask;
        }

        public DataConnectionStub()
        {
            Configuration = new DataConnectionConfiguration();

            Storage = new Storage { Id = Guid.NewGuid(), Address = "http://localhost", Name = "Data connection stub storage" };
            Account = new Account { Id = Guid.NewGuid(), Name = "test", Password = "123" };
            Space = new Space { Id = Guid.NewGuid(), AccountId = Account.Id, Name = "Data connection stub test space"};

            Entries = new EntryContextStub();
            Roots = new RootContextStub();
            Content = new ContentContextStub();
            Properties = new PropertiesContextStub();
        }
    }
}
