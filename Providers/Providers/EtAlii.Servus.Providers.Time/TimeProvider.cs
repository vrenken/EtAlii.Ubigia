// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Provisioning.Time
{
    using EtAlii.Servus.Api.Functional;

    public class TimeProvider : IProvider
    {
        public IProviderConfiguration Configuration { get { return _configuration; } }
        private readonly IProviderConfiguration _configuration;

        private readonly ITimeImporter _timeImporter;

        public TimeProvider(
            IProviderConfiguration configuration, 
            ITimeImporter timeImporter)
        {
            _configuration = configuration;
            _timeImporter = timeImporter;
        }

        public void Stop()
        {
            //_timeImporter.Stop();
        }

        public void Start()
        {
            //_timeImporter.Start();
        }
    }
}
