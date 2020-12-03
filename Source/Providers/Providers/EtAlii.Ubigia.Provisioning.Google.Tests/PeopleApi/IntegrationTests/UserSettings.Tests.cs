namespace EtAlii.Ubigia.Provisioning.Google.Tests
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Provisioning.Google.PeopleApi;
    using EtAlii.Ubigia.Provisioning.Tests;
    using Xunit;

    public class UserSettingsTests : IClassFixture<ProvisioningUnitTestContext>
    {
        private readonly ProvisioningUnitTestContext _testContext;

        public UserSettingsTests(ProvisioningUnitTestContext testContext)
        {
            _testContext = testContext;
        }


        [Fact]
        public void UserSettings_UserSettingsSetter_Create()
        {
            // Arrange.

            // Act.
            var userSettingsSetter = new UserSettingsSetter();

            // Assert.
            Assert.NotNull(userSettingsSetter);
        }

        [Fact]
        public async Task UserSettings_UserSettingsSetter_Set_01_Null()
        {
            // Arrange.
            var userSettingsSetter = new UserSettingsSetter();
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();
            var email = Guid.NewGuid().ToString();
            var managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection().ConfigureAwait(false);
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.User).ConfigureAwait(false);
            await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.Configuration).ConfigureAwait(false);
            var context = await _testContext.ProvisioningTestContext.CreateScriptContext(accountName, password, spaceName).ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async() =>
            {
                await userSettingsSetter.Set(context, email, null).ConfigureAwait(false);
            });

            // Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(act).ConfigureAwait(false);
        }

        [Fact]
        public async Task UserSettings_UserSettingsSetter_Set_02()
        {
            // Arrange.
            var userSettingsSetter = new UserSettingsSetter();
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();
            var email = Guid.NewGuid().ToString();
            var managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection().ConfigureAwait(false);
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.User).ConfigureAwait(false);
            await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.Configuration).ConfigureAwait(false);
            var context = await _testContext.ProvisioningTestContext.CreateScriptContext(accountName, password, spaceName).ConfigureAwait(false);
            var userSettings = TestUserSettings.Create(email);

            // Act.
            await userSettingsSetter.Set(context, email, userSettings).ConfigureAwait(false);

            // Assert.
            var processingResult = await context.Process($"<= /Providers/Google/PeopleApi/\"{email}\"");
            dynamic result = await processingResult.Output.LastOrDefaultAsync();
            TestUserSettings.AreEqual(userSettings, result);
        }


        [Fact]
        public async Task UserSettings_UserSettingsSetter_Set_03_Twice()
        {
            // Arrange.
            var userSettingsSetter = new UserSettingsSetter();
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();
            var email = Guid.NewGuid().ToString();
            var managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection().ConfigureAwait(false);
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.User).ConfigureAwait(false);
            await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.Configuration).ConfigureAwait(false);
            var context = await _testContext.ProvisioningTestContext.CreateScriptContext(accountName, password, spaceName).ConfigureAwait(false);
            var firstUserSettings = TestUserSettings.Create(email);
            var secondUserSettings = TestUserSettings.Create(email);

            // Act.
            await userSettingsSetter.Set(context, email, firstUserSettings).ConfigureAwait(false);
            var processingResult = await context.Process($"<= /Providers/Google/PeopleApi/\"{email}\"");
            dynamic firstResult = await processingResult.Output.LastOrDefaultAsync();
            await userSettingsSetter.Set(context, email, secondUserSettings).ConfigureAwait(false);
            processingResult = await context.Process($"<= /Providers/Google/PeopleApi/\"{email}\"");
            dynamic secondResult = await processingResult.Output.LastOrDefaultAsync();

            // Assert.
            TestUserSettings.AreEqual(firstUserSettings, firstResult);
            TestUserSettings.AreEqual(secondUserSettings, secondResult);
        }

        [Fact]
        public async Task UserSettings_UserSettingsSetter_Set_04_Twice_No_Inbetween_Read()
        {
            // Arrange.
            var userSettingsSetter = new UserSettingsSetter();
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();
            var email = Guid.NewGuid().ToString();
            var managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection().ConfigureAwait(false);
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.User).ConfigureAwait(false);
            await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.Configuration).ConfigureAwait(false);
            var context = await _testContext.ProvisioningTestContext.CreateScriptContext(accountName, password, spaceName).ConfigureAwait(false);
            var firstUserSettings = TestUserSettings.Create(email);
            var secondUserSettings = TestUserSettings.Create(email);

            // Act.
            await userSettingsSetter.Set(context, email, firstUserSettings).ConfigureAwait(false);
            await userSettingsSetter.Set(context, email, secondUserSettings).ConfigureAwait(false);
            var processingResult = await context.Process($"<= /Providers/Google/PeopleApi/\"{email}\"");
            dynamic secondResult = await processingResult.Output.LastOrDefaultAsync();

            // Assert.
            TestUserSettings.AreEqual(secondUserSettings, secondResult);
        }

        [Fact]
        public async Task UserSettings_UserSettingsSetter_Set_05_Twice_Check_With_UserSettingsGetter()
        {
            // Arrange.
            var userSettingsSetter = new UserSettingsSetter();
            var userSettingsGetter = new UserSettingsGetter();
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();
            var email = Guid.NewGuid().ToString();
            var managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection().ConfigureAwait(false);
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.User).ConfigureAwait(false);
            await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.Configuration).ConfigureAwait(false);
            var context = await _testContext.ProvisioningTestContext.CreateScriptContext(accountName, password, spaceName).ConfigureAwait(false);
            var firstUserSettings = TestUserSettings.Create(email);
            var secondUserSettings = TestUserSettings.Create(email);

            // Act.
            await userSettingsSetter.Set(context, email, firstUserSettings).ConfigureAwait(false);
            var firstResult = (await userSettingsGetter.Get(context).ConfigureAwait(false)).Single(s => s.Email == email);
            await userSettingsSetter.Set(context, email, secondUserSettings).ConfigureAwait(false);
            var secondResult = (await userSettingsGetter.Get(context).ConfigureAwait(false)).Single(s => s.Email == email);

            // Assert.
            TestUserSettings.AreEqual(firstUserSettings, firstResult);
            TestUserSettings.AreEqual(secondUserSettings, secondResult);
        }

        [Fact]
        public async Task UserSettings_UserSettingsSetter_Set_06_Twice_No_Inbetween_Read_Check_With_UserSettingsGetter()
        {
            // Arrange.
            var userSettingsSetter = new UserSettingsSetter();
            var userSettingsGetter = new UserSettingsGetter();
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();
            var email = Guid.NewGuid().ToString();
            var managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection().ConfigureAwait(false);
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.User).ConfigureAwait(false);
            await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.Configuration).ConfigureAwait(false);
            var context = await _testContext.ProvisioningTestContext.CreateScriptContext(accountName, password, spaceName).ConfigureAwait(false);
            var firstUserSettings = TestUserSettings.Create(email);
            var secondUserSettings = TestUserSettings.Create(email);

            // Act.
            await userSettingsSetter.Set(context, email, firstUserSettings).ConfigureAwait(false);
            await userSettingsSetter.Set(context, email, secondUserSettings).ConfigureAwait(false);
            var secondResult = (await userSettingsGetter.Get(context).ConfigureAwait(false)).Single(s => s.Email == email);

            // Assert.
            TestUserSettings.AreEqual(secondUserSettings, secondResult);
        }
    }
}
