// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Threading.Tasks;

    public class PeopleApiUpdater : AsyncProcess, IPeopleApiUpdater
    {
        private readonly IConfigurationSpaceGetter _configurationSpaceGetter;
        private readonly IPeopleApiConfigurationSpaceUpdater _peopleApiConfigurationSpaceUpdater;
        private readonly ISystemSettingsGetter _systemSettingsGetter;
        private readonly IProviderContext _context;

        public PeopleApiUpdater(
            IConfigurationSpaceGetter configurationSpaceGetter, 
            IPeopleApiConfigurationSpaceUpdater peopleApiConfigurationSpaceUpdater, 
            ISystemSettingsGetter systemSettingsGetter, 
            IProviderContext context)
        {
            _configurationSpaceGetter = configurationSpaceGetter;
            _peopleApiConfigurationSpaceUpdater = peopleApiConfigurationSpaceUpdater;
            _systemSettingsGetter = systemSettingsGetter;
            _context = context;
        }

        protected override async Task Run()
        {
            var systemSettings = await _systemSettingsGetter.Get(_context.SystemScriptContext).ConfigureAwait(false);

            // Fetch all configuration spaces.
            var configurationSpaces = _configurationSpaceGetter.GetAll();
            await foreach (var configurationSpace in configurationSpaces)
            {
                // If so, update the People Api in this space.
                await _peopleApiConfigurationSpaceUpdater.Update(configurationSpace, systemSettings).ConfigureAwait(false);
            }
        }
    }
}