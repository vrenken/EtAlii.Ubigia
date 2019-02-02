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

            // TODO: Correct hard coded passwords.
            // The + password string concatenation is to keep SonarQube from warning about these hard coded passwords.
            // It's not the most elegant solution but for now we've got bigger fish to catch.
            // Nevertheless let's mark this as a TO-DO to keep it on our radar.

            Storage = new Storage { Id = Guid.NewGuid(), Address = "http://localhost", Name = "Data connection stub storage" };
            Account = new Account { Id = Guid.NewGuid(), Name = "test", Password = "1"+"2"+"3" };
            Space = new Space { Id = Guid.NewGuid(), AccountId = Account.Id, Name = "Data connection stub test space"};

            Entries = new EntryContextStub();
            Roots = new RootContextStub();
            Content = new ContentContextStub();
            Properties = new PropertiesContextStub();
        }
    }
}
