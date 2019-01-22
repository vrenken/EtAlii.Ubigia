namespace EtAlii.Ubigia.Provisioning.Google.Tests
{
    using System;
    using EtAlii.Ubigia.Provisioning.Google.PeopleApi;
    using Xunit;

    public static class TestUserSettings
    {
        public static UserSettings Create()
        {
            return Create(Guid.NewGuid().ToString());
        }

        public static UserSettings Create(string email)
        {
            var random = new Random();
            return new UserSettings
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = Guid.NewGuid().ToString(),
                DisplayNameLastFirst = Guid.NewGuid().ToString(),
                Email = email,
                GivenName = Guid.NewGuid().ToString(),
                FamilyName = Guid.NewGuid().ToString(),
                TokenType = Guid.NewGuid().ToString(),
                AccessToken = Guid.NewGuid().ToString(),
                RefreshToken = Guid.NewGuid().ToString(),
                ExpiresIn = TimeSpan.FromMinutes(random.Next(0, 10000)),
                Created = DateTime.FromFileTimeUtc(DateTime.UtcNow.ToFileTime() + random.Next(0, 1000000000)),
                Updated = DateTime.FromFileTimeUtc(DateTime.UtcNow.ToFileTime() + random.Next(0, 1000000000))
            };
        }

        public static void AreEqual(UserSettings expectedUserSettings, dynamic result)
        {
            Assert.NotNull(result);
            Assert.Equal(expectedUserSettings.Id, result.Id);
            Assert.Equal(expectedUserSettings.DisplayName, result.DisplayName);
            Assert.Equal(expectedUserSettings.DisplayNameLastFirst, result.DisplayNameLastFirst);
            Assert.Equal(expectedUserSettings.Email, result.Email);
            Assert.Equal(expectedUserSettings.GivenName, result.GivenName);
            Assert.Equal(expectedUserSettings.FamilyName, result.FamilyName);
            Assert.Equal(expectedUserSettings.TokenType, result.TokenType);
            Assert.Equal(expectedUserSettings.AccessToken, result.AccessToken);
            Assert.Equal(expectedUserSettings.RefreshToken, result.RefreshToken);
            Assert.Equal(expectedUserSettings.ExpiresIn, result.ExpiresIn);
            Assert.Equal(expectedUserSettings.Created.Year, result.Created.Year);
            Assert.Equal(expectedUserSettings.Created.Month, result.Created.Month);
            Assert.Equal(expectedUserSettings.Created.Day, result.Created.Day);
            Assert.Equal(expectedUserSettings.Created.Hour, result.Created.Hour);
            Assert.Equal(expectedUserSettings.Created.Second, result.Created.Second);
            Assert.Equal(expectedUserSettings.Created.Millisecond, result.Created.Millisecond);
            Assert.Equal(expectedUserSettings.Created.Kind, result.Created.Kind);
            Assert.Equal(expectedUserSettings.Created.Ticks, result.Created.Ticks);
            Assert.Equal(expectedUserSettings.Updated, result.Updated);
        }
    }
}
