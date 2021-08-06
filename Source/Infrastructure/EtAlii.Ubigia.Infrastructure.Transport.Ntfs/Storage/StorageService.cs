// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Ntfs
{
    using System.Text;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public class StorageService : ServiceBase, IStorageService
    {
        public IStorage Storage { get; }
        private readonly string _baseFolder;

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
            Status.Title = "Ubigia infrastructure NTFS storage subsystem";

            Status.Description = "Starting...";
            Status.Summary = "Starting Ubigia NTFS storage subsystem";

            await base.Start().ConfigureAwait(false);

            var sb = new StringBuilder();
            sb.AppendLine("All OK. Ubigia NTFS storage subsystem is operational using the folder specified below.");
            sb.AppendLine($"Base folder: {_baseFolder}");

            Status.Description = "Running";
            Status.Summary = sb.ToString();
        }

        public override async Task Stop()
        {
            Status.Description = "Stopping...";
            Status.Summary = "Stopping Ubigia NTFS storage subsystem";

            await base.Stop().ConfigureAwait(false);

            var sb = new StringBuilder();
            sb.AppendLine("Finished stopping Ubigia NTFS storage subsystem from the folder specified below.");
            sb.AppendLine($"Base folder: {_baseFolder}");

            Status.Description = "Stopped";
            Status.Summary = sb.ToString();
        }
    }
}
