namespace EtAlii.Ubigia.Provisioning.Google.Tests
{
    using System;
    using EtAlii.Ubigia.Provisioning.Google.PeopleApi;
    using Xunit;

    public static class TestSystemSettings
    {
        public static SystemSettings Create()
        {
            return new SystemSettings
            {
                AuthenticationProviderx509CertificateUrl = Guid.NewGuid().ToString(),
                AuthenticationUrl = Guid.NewGuid().ToString(),
                ClientId = Guid.NewGuid().ToString(),
                ClientSecret = Guid.NewGuid().ToString(),
                RedirectUrl = Guid.NewGuid().ToString(),
                ProjectId = Guid.NewGuid().ToString(),
                TokenUrl = Guid.NewGuid().ToString(),
            };
        }

        public static void AreEqual(SystemSettings expectedSystemSettings, dynamic result)
        {
            Assert.NotNull(result);
            Assert.Equal(expectedSystemSettings.ClientId, result.ClientId);
            Assert.Equal(expectedSystemSettings.AuthenticationProviderx509CertificateUrl, result.AuthenticationProviderx509CertificateUrl);
            Assert.Equal(expectedSystemSettings.AuthenticationUrl, result.AuthenticationUrl);
            Assert.Equal(expectedSystemSettings.ClientSecret, result.ClientSecret);
            Assert.Equal(expectedSystemSettings.ProjectId, result.ProjectId);
            Assert.Equal(expectedSystemSettings.RedirectUrl, result.RedirectUrl);
            Assert.Equal(expectedSystemSettings.TokenUrl, result.TokenUrl);
        }
    }
}
