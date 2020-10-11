// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Threading.Tasks;
    using Serilog;

    public class DebuggingPeopleApiConfigurationSpaceUpdater : IPeopleApiConfigurationSpaceUpdater
    {
        private readonly IPeopleApiConfigurationSpaceUpdater _decoree;
        private readonly ILogger _logger = Log.ForContext<IPeopleApiConfigurationSpaceUpdater>();

        public DebuggingPeopleApiConfigurationSpaceUpdater(IPeopleApiConfigurationSpaceUpdater decoree)
        {
            _decoree = decoree;
        }

        public async Task Update(ConfigurationSpace configurationSpace, SystemSettings systemSettings)
        {
            _logger.Information("Processing space: {accountName}/{spaceName}", configurationSpace.Account.Name, configurationSpace.Space.Name);

            await _decoree.Update(configurationSpace, systemSettings);

            _logger.Information("Processed space: {accountName}/{spaceName}", configurationSpace.Account.Name, configurationSpace.Space.Name);
        }
    }
}