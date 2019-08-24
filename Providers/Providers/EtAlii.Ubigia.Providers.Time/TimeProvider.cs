// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Time
{
    using System.Threading.Tasks;

    public class TimeProvider : IProvider
    {
        /// <summary>
        /// The Configuration used to instantiate this Provider.
        /// </summary>
        public IProviderConfiguration Configuration { get; }

        // ReSharper disable once NotAccessedField.Local
        private readonly ITimeImporter _timeImporter;

        public TimeProvider(
            IProviderConfiguration configuration, 
            ITimeImporter timeImporter)
        {
            Configuration = configuration;
            _timeImporter = timeImporter;
        }

        public Task Stop()
        {
            //_timeImporter.Stop()
            return Task.CompletedTask;
        }

        public Task Start()
        {
            //_timeImporter.Start()
            return Task.CompletedTask;
        }
    }
}
