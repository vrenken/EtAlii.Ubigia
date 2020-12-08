// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
	using System;
	using System.Threading.Tasks;
	using global::Google.Apis.PeopleService.v1;
	using global::Google.Apis.Services;

	public class PeopleDataSpaceUpdater : IPeopleDataSpaceUpdater
    {

		private readonly IProviderContext _context;
        private readonly IUserSettingsGetter _userSettingsGetter;
        private readonly IPersonSetter _personSetter;
        private readonly TimeSpan _updateThreshold = TimeSpan.FromHours(1); 

        //private readonly string[] _contactScopes = new[] [ "https://www.googleapis.com/auth/contacts.readonly" ];     // view your basic profile info.

        public PeopleDataSpaceUpdater(
            IProviderContext context,
            IUserSettingsGetter userSettingsGetter, 
            IPersonSetter personSetter)
        {
            _userSettingsGetter = userSettingsGetter;
            _personSetter = personSetter;
            _context = context;
        }

        public async Task Update(ConfigurationSpace configurationSpace, SystemSettings systemSettings)
        {
	        var userConfigurationScriptContext = await _context.CreateScriptContext(configurationSpace.Space).ConfigureAwait(false);
	        
            var allUserSettings = await _userSettingsGetter.Get(userConfigurationScriptContext).ConfigureAwait(false);
            foreach (var userSettings in allUserSettings)
            {
                // We don't want to update using deprecated settings, so let's only use them when they are still fresh.
                if (DateTime.UtcNow - userSettings.Updated < _updateThreshold)
                {
                    await UpdateDataSpace(configurationSpace, systemSettings, userSettings).ConfigureAwait(false);
                }
            }
        }

        private async Task UpdateDataSpace(ConfigurationSpace configurationSpace, SystemSettings systemSettings, UserSettings userSettings)
        {
	        var userDataScriptContext = await _context.CreateScriptContext(configurationSpace.Account.Name, SpaceName.Data).ConfigureAwait(false);

            var request = CreateRequest(systemSettings, userSettings);
	        var feed = request.Execute();//.GetContacts()
            //feed.AutoPaging = true
            foreach (var person in feed.Connections)//.Entries)
            {
		        await _personSetter.Set(userDataScriptContext, person).ConfigureAwait(false);
			}
        }

	    //private ContactsRequest CreateRequest(SystemSettings systemSettings, UserSettings userSettings)
	    //[
		   // var parameters = new OAuth2Parameters()
		   // parameters.AccessToken = userSettings.AccessToken
		   // parameters.RefreshToken = userSettings.RefreshToken

		   // var settings = new RequestSettings(systemSettings.ClientId, parameters)
		   // var request = new ContactsRequest(settings)
		   // return request
	    //]
		private PeopleResource.ConnectionsResource.ListRequest CreateRequest(SystemSettings systemSettings, UserSettings userSettings)
		{
			// ReSharper disable once UnusedVariable
			var parameters = new global::Google.Apis.Auth.OAuth2.JsonCredentialParameters
			{
				ClientId = systemSettings.ClientId,
				PrivateKey = userSettings.AccessToken,
				RefreshToken = userSettings.RefreshToken
			};

	        var initializer = new BaseClientService.Initializer();

#pragma warning disable CA2000 // The lifetime of this service is handled by the ListRequest.  
            var service = new PeopleServiceService(initializer);
#pragma warning restore CA2000   
			//var builder = new global::Google.Apis.Requests.RequestBuilder()
	        return new PeopleResource.ConnectionsResource.ListRequest(service, "people/me");
            
			//builder.
			//var parameters = new OAuth2Parameters()
			//         parameters.AccessToken = userSettings.AccessToken
			//         parameters.RefreshToken = userSettings.RefreshToken

			//var settings = new RequestSettings(systemSettings.ClientId, parameters)
            //var request = new ContactsRequest(settings)
            //return request
        }
	}
}