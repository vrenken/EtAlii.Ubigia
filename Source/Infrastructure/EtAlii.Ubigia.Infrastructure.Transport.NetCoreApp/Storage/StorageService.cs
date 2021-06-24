// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.NetCoreApp
{
    using System.Text;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public class StorageService : ServiceBase, IStorageService
    {
        private readonly string _baseFolder;
        public IStorage Storage { get; }

        public StorageService(IConfigurationSection configurationSection, IStorage storage)
            : base(configurationSection)
        {
            Storage = storage;
            string baseFolder;
            baseFolder = configurationSection.GetValue<string>(nameof(baseFolder));
            _baseFolder = baseFolder;
        }

        public override async Task Start()
        {
            Status.Title = "Ubigia infrastructure NetCore storage subsystem";

            Status.Description = "Starting...";
            Status.Summary = "Starting Ubigia NetCore storage subsystem";

            await base.Start().ConfigureAwait(false);

            var sb = new StringBuilder();
            sb.AppendLine("All OK. Ubigia NetCore storage subsystem is operational using the folder specified below.");
            sb.AppendLine($"Base folder: {_baseFolder}");

            Status.Description = "Running";
            Status.Summary = sb.ToString();
        }

        public override async Task Stop()
        {
            Status.Description = "Stopping...";
            Status.Summary = "Stopping Ubigia NetCore storage subsystem";

            await base.Stop().ConfigureAwait(false);

            var sb = new StringBuilder();
            sb.AppendLine("Finished stopping Ubigia NetCore storage subsystem from the folder specified below.");
            sb.AppendLine($"Base folder: {_baseFolder}");

            Status.Description = "Stopped";
            Status.Summary = sb.ToString();
        }
    }
}
