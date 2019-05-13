// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System;
    using System.Threading.Tasks;

    public class PeopleApiConfigurationSpaceUpdater : IPeopleApiConfigurationSpaceUpdater
    {
        private readonly IProviderContext _context;
        private readonly IUserSettingsGetter _userSettingsGetter;
        private readonly IUserSettingsUpdater _userSettingsUpdater;
        private readonly TimeSpan _thresholdBeforeExpiration = TimeSpan.FromMinutes(5);


        public PeopleApiConfigurationSpaceUpdater(
            IProviderContext context, 
            IUserSettingsGetter userSettingsGetter, 
            IUserSettingsUpdater userSettingsUpdater)
        {
            _context = context;
            _userSettingsGetter = userSettingsGetter;
            _userSettingsUpdater = userSettingsUpdater;
        }

        public async Task Update(ConfigurationSpace configurationSpace, SystemSettings systemSettings)
        {
            var userDataScriptContext = await _context.CreateScriptContext(configurationSpace.Space);
            {
                var allUserSettings = await _userSettingsGetter.Get(userDataScriptContext);
                foreach (var userSettings in allUserSettings)
                {
                    var duration = userSettings.ExpiresIn - _thresholdBeforeExpiration;
                    duration = duration.TotalMilliseconds > 0 ? duration : userSettings.ExpiresIn;
                    //var duration = TimeSpan.FromMinutes(2) 
                    if (userSettings.Updated + duration < DateTime.UtcNow)
                    {
                        await _userSettingsUpdater.Update(userSettings, systemSettings, userDataScriptContext, _thresholdBeforeExpiration);
                    }
                }
            }
        }

    }
}