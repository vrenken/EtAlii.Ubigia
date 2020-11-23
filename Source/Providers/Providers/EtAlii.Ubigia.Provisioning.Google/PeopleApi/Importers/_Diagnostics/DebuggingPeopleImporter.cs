// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System;
    using System.Threading.Tasks;
    using Serilog;

    public class DebuggingPeopleImporter : IPeopleImporter
    {
        private readonly IPeopleImporter _decoree;
        private readonly ILogger _logger = Log.ForContext<IPeopleImporter>();

        public DebuggingPeopleImporter(IPeopleImporter decoree)
        {
            _decoree = decoree;
            _decoree.Error += OnError;
        }

        private void OnError(Exception exception)
        {
            _logger.Error(exception, "Exception PeopleImporter");
        }

        public async Task Start()
        {
            _logger.Information("Starting PeopleImporter");

            await _decoree.Start().ConfigureAwait(false);

            _logger.Information("Started PeopleImporter");
        }

        public async Task Stop()
        {
            _logger.Information("Stopping PeopleImporter");

            await _decoree.Stop().ConfigureAwait(false);

            _logger.Information("Stopped PeopleImporter");
        }

        public event Action<Exception> Error { add => _decoree.Error += value; remove => _decoree.Error -= value;
        }
    }
}