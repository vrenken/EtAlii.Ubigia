// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System;
    using EtAlii.Ubigia.Api.Transport;
    using global::Google.Contacts;
    using global::Google.GData.Client;

    public class PeopleDataSpaceUpdater : IPeopleDataSpaceUpdater
    {
        private readonly IProviderContext _context;
        private readonly IUserSettingsGetter _userSettingsGetter;
        private readonly IPersonSetter _personSetter;
        private readonly TimeSpan _updateThreshold = TimeSpan.FromHours(1); 

        private readonly string[] _contactScopes = new string[] { "https://www.googleapis.com/auth/contacts.readonly" };     // view your basic profile info.

        public PeopleDataSpaceUpdater(
            IProviderContext context,
            IUserSettingsGetter userSettingsGetter, 
            IPersonSetter personSetter)
        {
            _userSettingsGetter = userSettingsGetter;
            _personSetter = personSetter;
            _context = context;
        }

        public void Update(ConfigurationSpace configurationSpace, SystemSettings systemSettings)
        {
            using (var userConfigurationContext = _context.CreateDataContext(configurationSpace.Space))
            {
                var allUserSettings = _userSettingsGetter.Get(userConfigurationContext);
                foreach (var userSettings in allUserSettings)
                {
                    // We don't want to update using deprecated settings, so let's only use them when they are still fresh.
                    if (DateTime.UtcNow - userSettings.Updated < _updateThreshold)
                    {
                        UpdateDataSpace(configurationSpace, systemSettings, userSettings);
                    }
                }
            }
        }

        private void UpdateDataSpace(ConfigurationSpace configurationSpace, SystemSettings systemSettings, UserSettings userSettings)
        {
            using (var userDataContext = _context.CreateDataContext(configurationSpace.Account.Name, SpaceName.Data))
            {
                var request = CreateRequest(systemSettings, userSettings);
                var feed = request.GetContacts();
                feed.AutoPaging = true;
                foreach (var person in feed.Entries)
                {
                    _personSetter.Set(userDataContext , person);
                }
            }
        }

        private ContactsRequest CreateRequest(SystemSettings systemSettings, UserSettings userSettings)
        {
            var parameters = new OAuth2Parameters();
            parameters.AccessToken = userSettings.AccessToken;
            parameters.RefreshToken = userSettings.RefreshToken;

            var settings = new RequestSettings(systemSettings.ClientId, parameters);
            var request = new ContactsRequest(settings);
            return request;
        }
    }
}