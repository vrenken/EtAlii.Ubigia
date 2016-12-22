// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Provisioning.Google.PeopleApi
{
    using EtAlii.xTechnology.Logging;

    public class DebuggingPeopleApiConfigurationSpaceUpdater : IPeopleApiConfigurationSpaceUpdater
    {
        private readonly IPeopleApiConfigurationSpaceUpdater _decoree;
        private readonly ILogger _logger;

        public DebuggingPeopleApiConfigurationSpaceUpdater(IPeopleApiConfigurationSpaceUpdater decoree, ILogger logger)
        {
            _decoree = decoree;
            _logger = logger;
        }

        public void Update(ConfigurationSpace configurationSpace, SystemSettings systemSettings)
        {
            _logger.Info($"Processing space: {configurationSpace.Account.Name}/{configurationSpace.Space.Name}");

            _decoree.Update(configurationSpace, systemSettings);

            _logger.Info($"Processed space: {configurationSpace.Account.Name}/{configurationSpace.Space.Name}");
        }
    }
}