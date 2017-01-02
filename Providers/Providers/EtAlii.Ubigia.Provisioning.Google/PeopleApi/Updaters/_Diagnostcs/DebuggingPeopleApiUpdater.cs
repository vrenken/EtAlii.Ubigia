// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System;
    using EtAlii.xTechnology.Logging;

    public class DebuggingPeopleApiUpdater : IPeopleApiUpdater
    {
        private readonly IPeopleApiUpdater _decoree;
        private readonly ILogger _logger;

        public DebuggingPeopleApiUpdater(IPeopleApiUpdater decoree, ILogger logger)
        {
            _decoree = decoree;
            _decoree.Error += OnError;
            _logger = logger;
        }

        private void OnError(Exception exception)
        {
            _logger.Critical("Exception in PeopleApiUpdater", exception);
        }

        public void Start()
        {
            _logger.Info("Starting PeopleApiUpdater");

            _decoree.Start();

            _logger.Info("Started PeopleApiUpdater");
        }

        public void Stop()
        {
            _logger.Info("Stopping PeopleApiUpdater");

            _decoree.Stop();

            _logger.Info("Stopped PeopleApiUpdater");
        }

        public event Action<Exception> Error { add { _decoree.Error += value; } remove { _decoree.Error -= value; } }
    }
}