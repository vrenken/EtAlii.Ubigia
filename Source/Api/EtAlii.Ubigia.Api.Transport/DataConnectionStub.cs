// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    public class DataConnectionStub : IDataConnection
    {
        /// <inheritdoc />
        public Storage Storage { get; }

        /// <inheritdoc />
        public Account Account { get; }

        /// <inheritdoc />
        public Space Space { get; }

        /// <inheritdoc />
        public IEntryContext Entries { get; }

        /// <inheritdoc />
        public IRootContext Roots { get; }

        /// <inheritdoc />
        public IContentContext Content { get; }

        /// <inheritdoc />
        public IPropertiesContext Properties { get; }

        /// <inheritdoc />
        public bool IsConnected { get; } = false;

        /// <summary>
        /// The Configuration used to instantiate this DataConnection.
        /// </summary>
        public IDataConnectionConfiguration Configuration { get; }

        /// <inheritdoc />
        public Task Open()
        {
            return Task.CompletedTask;
        }
        /// <inheritdoc />
        public Task Close()
        {
            return Task.CompletedTask;
        }

        /// Create a new <see cref="DataConnectionStub" /> instance.
        [SuppressMessage("Sonar Code Smell", "S2068:Credentials should not be hard-coded", Justification = "This is a stub, only needed for testing/stubbing purposes.")]
        public DataConnectionStub()
        {
            Configuration = new DataConnectionConfiguration();
        
            Storage = new Storage {Id = Guid.NewGuid(), Address = "http://localhost", Name = "Data connection stub storage"};
            Account = new Account { Id = Guid.NewGuid(), Name = "test", Password = "123" };
            Space = new Space { Id = Guid.NewGuid(), AccountId = Account.Id, Name = "Data connection stub test space"};

            Entries = new EntryContextStub();
            Roots = new RootContextStub();
            Content = new ContentContextStub();
            Properties = new PropertiesContextStub();
        }
    }
}
