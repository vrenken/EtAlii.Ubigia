// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.InMemory
{
    using System.Text;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public class StorageService : ServiceBase, IStorageService
    {
        public IStorage Storage { get; }

        public StorageService(IConfigurationSection configurationSection,  IStorage storage)
            : base(configurationSection)
        {
            Storage = storage;
        }

        public override async Task Start()
        {
            Status.Title = "Ubigia infrastructure in-memory storage subsystem";

            Status.Description = "Starting...";
            Status.Summary = "Starting Ubigia in-memory storage subsystem";

            await base.Start().ConfigureAwait(false);

            var sb = new StringBuilder();
            sb.AppendLine("All OK. Ubigia in-memory storage subsystem is operational.");

            Status.Description = "Running";
            Status.Summary = sb.ToString();
        }

        public override async Task Stop()
        {
            Status.Description = "Stopping...";
            Status.Summary = "Stopping Ubigia in-memory storage subsystem";

            await base.Stop().ConfigureAwait(false);

            var sb = new StringBuilder();
            sb.AppendLine("Finished stopping Ubigia in-memory storage subsystem.");

            Status.Description = "Stopped";
            Status.Summary = sb.ToString();
        }
    }
}
