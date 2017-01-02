// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
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

        protected override void Run()
        {
            var systemSettings = _systemSettingsGetter.Get(_context.SystemDataContext);

            // Fetch all configuration spaces.
            var configurationSpaces = _configurationSpaceGetter.GetAll();
            foreach (var configurationSpace in configurationSpaces)
            {
                // If so, update the People Api in this space.
                _peopleApiConfigurationSpaceUpdater.Update(configurationSpace, systemSettings);
            }
        }
    }
}