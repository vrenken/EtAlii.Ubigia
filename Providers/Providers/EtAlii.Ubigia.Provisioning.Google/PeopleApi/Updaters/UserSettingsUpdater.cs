// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;
    using global::Google.Apis.Auth.OAuth2;
    using global::Google.Apis.Auth.OAuth2.Flows;

    public class UserSettingsUpdater : IUserSettingsUpdater
    {
        private readonly IUserSettingsSetter _userSettingsSetter;

        public UserSettingsUpdater(IUserSettingsSetter userSettingsSetter)
        {
            _userSettingsSetter = userSettingsSetter;
        }

        public async Task Update(UserSettings userSettings, SystemSettings systemSettings, IGraphSLScriptContext userDataContext, TimeSpan thresholdBeforeExpiration)
        {
            var clientSecrets = new ClientSecrets
            {
                ClientSecret = systemSettings.ClientSecret, 
                ClientId = systemSettings.ClientId
            };

            var initializer = new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = clientSecrets,
                Scopes = new[] { "https://www.googleapis.com/auth/plus.login" }
            };

            using (var flow = new AuthorizationCodeFlow(initializer))
            {
                var result = await flow.RefreshTokenAsync(userSettings.Id, userSettings.RefreshToken, CancellationToken.None);
                userSettings.RefreshToken = result.RefreshToken;
                userSettings.AccessToken = result.AccessToken;
                userSettings.ExpiresIn = TimeSpan.FromSeconds(result.ExpiresInSeconds ?? thresholdBeforeExpiration.TotalSeconds);
                userSettings.Updated = DateTime.UtcNow;
                await _userSettingsSetter.Set(userDataContext, userSettings.Email, userSettings);
            }
        }
    }
}