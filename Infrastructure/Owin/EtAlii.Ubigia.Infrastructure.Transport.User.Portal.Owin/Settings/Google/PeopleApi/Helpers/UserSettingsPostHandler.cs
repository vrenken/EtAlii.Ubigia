// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Transport;
    using EtAlii.Ubigia.Provisioning.Google.PeopleApi;

    public class UserSettingsPostHandler : IUserSettingsPostHandler
    {
        private readonly IUserSettingsSetter _userSettingsSetter;
        private readonly ISystemConnectionCreationProxy _connectionCreationProxy;
        private readonly IAuthenticationTokenConverter _authenticationTokenConverter;
        private readonly IGoogleAuthenticationTokenProvider _googleAuthenticationTokenProvider;
        private readonly IGoogleIdentityProvider _googleIdentityProvider;

        public UserSettingsPostHandler(
            IUserSettingsSetter userSettingsSetter, 
            ISystemConnectionCreationProxy connectionCreationProxy, 
            IAuthenticationTokenConverter authenticationTokenConverter, 
            IGoogleAuthenticationTokenProvider googleAuthenticationTokenProvider, 
            IGoogleIdentityProvider googleIdentityProvider)
        {
            _userSettingsSetter = userSettingsSetter;
            _connectionCreationProxy = connectionCreationProxy;
            _authenticationTokenConverter = authenticationTokenConverter;
            _googleAuthenticationTokenProvider = googleAuthenticationTokenProvider;
            _googleIdentityProvider = googleIdentityProvider;
        }

        public HttpResponseMessage Post(dynamic settings, HttpRequestMessage request, HttpActionContext actionContext)
        {
            var authenticationToken = _authenticationTokenConverter.FromHttpActionContext(actionContext);
            var userName = authenticationToken.Name;

            string authorizationCode = settings.authorization_code;

            // 1. convert code to token + refresh.
            var token = _googleAuthenticationTokenProvider.GetRefreshToken(authorizationCode);

            // 2. Acquire google ID, put this in username.
            var googleIdentity = _googleIdentityProvider.GetGoogleIdentity(token);// userName;

            // 3. Convert to UserSettings.
            var now = DateTime.UtcNow;
            var userSettings = new UserSettings
            {
                Id = googleIdentity.Id,
                DisplayName = googleIdentity.DisplayName,
                DisplayNameLastFirst = googleIdentity.DisplayNameLastFirst,
                FamilyName = googleIdentity.FamilyName,
                GivenName = googleIdentity.GivenName,
                Email = googleIdentity.Email,
                AccessToken = token.access_token,
                ExpiresIn = TimeSpan.FromSeconds(Convert.ToInt32(token.expires_in)),
                TokenType = token.token_type,
                RefreshToken = token.refresh_token,
                Created = now,
                Updated = now,
            };

            // 4. And store.
            var userConnection = _connectionCreationProxy.Request();
            IDataConnection spaceConnection = null;
            var task = Task.Run(async () =>
            {
                spaceConnection = await userConnection.OpenSpace(userName, SpaceName.Configuration);
            });
            task.Wait();
            var context = new DataContextFactory().Create(spaceConnection);
            _userSettingsSetter.Set(context, googleIdentity.Email, userSettings);

            return request.CreateResponse(HttpStatusCode.OK, userSettings);
        }
    }
}
