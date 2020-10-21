// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System;
    using System.Threading.Tasks;

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

            Interval = TimeSpan.FromMinutes(1);
        }

        protected override async Task Run()
        {
            var systemSettings = await _systemSettingsGetter.Get(_context.SystemScriptContext);

            // Fetch all configuration spaces.
            var configurationSpaces = _configurationSpaceGetter.GetAll();
            await foreach (var configurationSpace in configurationSpaces)
            {
                // If so, update the people for this user.
                await _spaceUpdater.Update(configurationSpace, systemSettings);
            }
        }
    }
}