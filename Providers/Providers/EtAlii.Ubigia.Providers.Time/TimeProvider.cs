// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Time
{
    public class TimeProvider : IProvider
    {
        public IProviderConfiguration Configuration { get; }

        private readonly ITimeImporter _timeImporter;

        public TimeProvider(
            IProviderConfiguration configuration, 
            ITimeImporter timeImporter)
        {
            Configuration = configuration;
            _timeImporter = timeImporter;
        }

        public void Stop()
        {
            // Stop the subprocesses related to importing time information.
        }

        public void Start()
        {
            // Start the subprocesses related to importing time information.
        }
    }
}
