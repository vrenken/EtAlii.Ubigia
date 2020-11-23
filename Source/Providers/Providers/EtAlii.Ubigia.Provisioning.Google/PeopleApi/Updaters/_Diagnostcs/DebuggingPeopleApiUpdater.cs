// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System;
    using System.Threading.Tasks;
    using Serilog;

    public class DebuggingPeopleApiUpdater : IPeopleApiUpdater
    {
        private readonly IPeopleApiUpdater _decoree;
        private readonly ILogger _logger = Log.ForContext<IPeopleApiUpdater>();

        public event Action<Exception> Error { add => _decoree.Error += value; remove => _decoree.Error -= value; }

        public DebuggingPeopleApiUpdater(IPeopleApiUpdater decoree)
        {
            _decoree = decoree;
            _decoree.Error += OnError;
        }

        private void OnError(Exception exception)
        {
            _logger.Error(exception, "Exception in PeopleApiUpdater");
        }

        public async Task Start()
        {
            _logger.Information("Starting PeopleApiUpdater");

            await _decoree.Start().ConfigureAwait(false);

            _logger.Information("Started PeopleApiUpdater");
        }

        public async Task Stop()
        {
            _logger.Information("Stopping PeopleApiUpdater");

            await _decoree.Stop().ConfigureAwait(false);

            _logger.Information("Stopped PeopleApiUpdater");
        }
    }
}