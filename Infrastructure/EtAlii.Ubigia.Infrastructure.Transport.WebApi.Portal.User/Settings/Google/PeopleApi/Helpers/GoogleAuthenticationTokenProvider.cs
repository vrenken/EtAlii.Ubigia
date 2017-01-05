// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Infrastructure.Transport.WebApi.Portal.Admin
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Transport;
    using EtAlii.Ubigia.Provisioning.Google.PeopleApi;
    using Newtonsoft.Json;

    public class GoogleAuthenticationTokenProvider : IGoogleAuthenticationTokenProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ISystemSettingsGetter _systemSettingsGetter;
        private readonly ISystemConnectionCreationProxy _connectionCreationProxy;
        private readonly ISerializer _serializer;

        public GoogleAuthenticationTokenProvider(
            ISystemSettingsGetter systemSettingsGetter, 
            ISystemConnectionCreationProxy connectionCreationProxy, 
            ISerializer serializer)
        {
            _systemSettingsGetter = systemSettingsGetter;
            _connectionCreationProxy = connectionCreationProxy;
            _serializer = serializer;
            _httpClient = new HttpClient();
        }

        public GoogleAuthenticationToken GetRefreshToken(string authorizationCode)
        {
            GoogleAuthenticationToken token = null;

            var task = Task.Run(async () =>
            {
                var systemSettings = GetSystemSettings();

                var keyValuePairs = new[]
                {
                    new KeyValuePair<string, string>("code", authorizationCode),
                    new KeyValuePair<string, string>("client_id", systemSettings.ClientId),
                    new KeyValuePair<string, string>("client_secret", systemSettings.ClientSecret),
                    new KeyValuePair<string, string>("redirect_uri", systemSettings.RedirectUrl),
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                };
                var content = new FormUrlEncodedContent(keyValuePairs);

                var response = await _httpClient.PostAsync("https://www.googleapis.com/oauth2/v4/token", content);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                using (var textReader = new StringReader(result))
                {
                    using (var jsonReader = new JsonTextReader(textReader))
                    {
                        token = _serializer.Deserialize<GoogleAuthenticationToken>(jsonReader);
                    }
                }
            });
            task.Wait();
            return token;
        }

        private SystemSettings GetSystemSettings()
        {
            var systemConnection = _connectionCreationProxy.Request();
            IDataConnection spaceConnection = null;
            var task = Task.Run(async () =>
            {
                spaceConnection = await systemConnection.OpenSpace(AccountName.System, SpaceName.System);
            });
            task.Wait();
            var context = new DataContextFactory().Create(spaceConnection);
            var settings = _systemSettingsGetter.Get(context);
            return settings;
        }
        //  
        // code
        // client_id
        //client_secret
        // redirect_uri
        // grant_type=authorization_code

        //POST /oauth2/v4/token HTTP/1.1
        //Host: www.googleapis.com
        //Content-Type: application/x-www-form-urlencoded

        //code=4/P7q7W91a-oMsCeLvIaQm6bTrgtp7&
        //client_id=8819981768.apps.googleusercontent.com&
        //client_secret={client_secret}&
        //redirect_uri=https://oauth2-login-demo.appspot.com/code&
        //grant_type=authorization_code

        //{
        //    "access_token":"1/fFAGRNJru1FTz70BzhT3Zg",
        //    "expires_in":3920,
        //    "token_type":"Bearer"
        //}
    }
}