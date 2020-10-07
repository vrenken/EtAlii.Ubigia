// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Diagnostics;

    public class DebuggingPeopleImporter : IPeopleImporter
    {
        private readonly IPeopleImporter _decoree;
        private readonly ILogger _logger;

        public DebuggingPeopleImporter(IPeopleImporter decoree, ILogger logger)
        {
            _decoree = decoree;
            _decoree.Error += OnError;
            _logger = logger;
        }

        private void OnError(Exception exception)
        {
            _logger.Critical("Exception PeopleImporter", exception);
        }

        public async Task Start()
        {
            _logger.Info("Starting PeopleImporter");

            await _decoree.Start();

            _logger.Info("Started PeopleImporter");
        }

        public async Task Stop()
        {
            _logger.Info("Stopping PeopleImporter");

            await _decoree.Stop();

            _logger.Info("Stopped PeopleImporter");
        }

        public event Action<Exception> Error { add => _decoree.Error += value; remove => _decoree.Error -= value;
        }
    }
}