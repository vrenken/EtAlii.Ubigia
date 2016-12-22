namespace EtAlii.Servus.Provisioning.Google.PeopleApi
{
    using System;

    public class UserSettings
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string DisplayNameLastFirst { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public TimeSpan ExpiresIn { get; set; }
        public string TokenType { get; set; }
        public string RefreshToken { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
