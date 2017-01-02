// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System;

    public class PeopleImporter : AsyncProcess, IPeopleImporter
    {
        private readonly IConfigurationSpaceGetter _configurationSpaceGetter;
        private readonly ISystemSettingsGetter _systemSettingsGetter;
        private readonly IProviderContext _context;
        private readonly IPeopleDataSpaceUpdater _spaceUpdater;

        public PeopleImporter(
            IProviderContext context, 
            ISystemSettingsGetter systemSettingsGetter, 
            IConfigurationSpaceGetter configurationSpaceGetter, 
            IPeopleDataSpaceUpdater spaceUpdater)
        {
            _context = context;
            _systemSettingsGetter = systemSettingsGetter;
            _configurationSpaceGetter = configurationSpaceGetter;
            _spaceUpdater = spaceUpdater;

            this.Interval = TimeSpan.FromMinutes(1);
        }

        protected override void Run()
        {
            var systemSettings = _systemSettingsGetter.Get(_context.SystemDataContext);

            // Fetch all configuration spaces.
            var configurationSpaces = _configurationSpaceGetter.GetAll();
            foreach (var configurationSpace in configurationSpaces)
            {
                // If so, update the people for this user.
                _spaceUpdater.Update(configurationSpace, systemSettings);
            }

        }
    }
}