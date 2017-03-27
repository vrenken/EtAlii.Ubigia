namespace EtAlii.Ubigia.Provisioning.Google.Tests
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Provisioning.Google.PeopleApi;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    
    public class UserSettings_Tests : IClassFixture<ProvisioningUnitTestContext>
    {
        private readonly ProvisioningUnitTestContext _testContext;

        public UserSettings_Tests(ProvisioningUnitTestContext testContext)
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
            var managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection();
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.User);
            await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.Configuration);
            var context = await _testContext.ProvisioningTestContext.CreateDataContext(accountName, password, spaceName);

            // Act.
            var act = new Action(() =>
            {
                userSettingsSetter.Set(context, email, null);
            });

            // Assert.
            ExceptionAssert.Throws<ArgumentNullException>(act);
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
            var managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection();
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.User);
            await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.Configuration);
            var context = await _testContext.ProvisioningTestContext.CreateDataContext(accountName, password, spaceName);
            var userSettings = TestUserSettings.Create(email);

            // Act.
            userSettingsSetter.Set(context, email, userSettings);

            // Assert.
            var processingResult = await context.Scripts.Process($"<= /Providers/Google/PeopleApi/\"{email}\"");
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
            var managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection();
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.User);
            await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.Configuration);
            var context = await _testContext.ProvisioningTestContext.CreateDataContext(accountName, password, spaceName);
            var firstUserSettings = TestUserSettings.Create(email);
            var secondUserSettings = TestUserSettings.Create(email);

            // Act.
            userSettingsSetter.Set(context, email, firstUserSettings);
            var processingResult = await context.Scripts.Process($"<= /Providers/Google/PeopleApi/\"{email}\"");
            dynamic firstResult = await processingResult.Output.LastOrDefaultAsync();
            userSettingsSetter.Set(context, email, secondUserSettings);
            processingResult = await context.Scripts.Process($"<= /Providers/Google/PeopleApi/\"{email}\"");
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
            var managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection();
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.User);
            await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.Configuration);
            var context = await _testContext.ProvisioningTestContext.CreateDataContext(accountName, password, spaceName);
            var firstUserSettings = TestUserSettings.Create(email);
            var secondUserSettings = TestUserSettings.Create(email);

            // Act.
            userSettingsSetter.Set(context, email, firstUserSettings);
            userSettingsSetter.Set(context, email, secondUserSettings);
            var processingResult = await context.Scripts.Process($"<= /Providers/Google/PeopleApi/\"{email}\"");
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
            var managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection();
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.User);
            await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.Configuration);
            var context = await _testContext.ProvisioningTestContext.CreateDataContext(accountName, password, spaceName);
            var firstUserSettings = TestUserSettings.Create(email);
            var secondUserSettings = TestUserSettings.Create(email);

            // Act.
            userSettingsSetter.Set(context, email, firstUserSettings);
            var firstResult = userSettingsGetter.Get(context).Single(s => s.Email == email);
            userSettingsSetter.Set(context, email, secondUserSettings);
            var secondResult = userSettingsGetter.Get(context).Single(s => s.Email == email);

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
            var managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection();
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.User);
            await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.Configuration);
            var context = await _testContext.ProvisioningTestContext.CreateDataContext(accountName, password, spaceName);
            var firstUserSettings = TestUserSettings.Create(email);
            var secondUserSettings = TestUserSettings.Create(email);

            // Act.
            userSettingsSetter.Set(context, email, firstUserSettings);
            userSettingsSetter.Set(context, email, secondUserSettings);
            var secondResult = userSettingsGetter.Get(context).Single(s => s.Email == email);

            // Assert.
            TestUserSettings.AreEqual(secondUserSettings, secondResult);
        }
    }
}
