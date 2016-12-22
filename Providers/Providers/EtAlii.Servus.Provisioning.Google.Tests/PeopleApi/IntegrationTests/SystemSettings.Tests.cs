namespace EtAlii.Servus.Provisioning.Google.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Provisioning.Google.PeopleApi;
    using EtAlii.Servus.Provisioning.Tests;
    using EtAlii.Servus.Tests;
    using Xunit;

    
    public class SystemSettings_Tests : IClassFixture<ProvisioningUnitTestContext>
    {
        private readonly ProvisioningUnitTestContext _testContext;

        public SystemSettings_Tests(ProvisioningUnitTestContext testContext)
        {
            this._testContext = testContext;
        }

        [Fact]
        public void SystemSettings_SystemSettingsGetter_Create()
        {
            // Arrange.

            // Act.
            var systemSettingsGetter = new SystemSettingsGetter();

            // Assert.
            Assert.NotNull(systemSettingsGetter);

        }

        [Fact]
        public void SystemSettings_SystemSettingsSetter_Create()
        {
            // Arrange.

            // Act.
            var systemSettingsSetter = new SystemSettingsSetter();

            // Assert.
            Assert.NotNull(systemSettingsSetter);
        }

        [Fact]
        public async Task SystemSettings_SystemSettingsSetter_Set_01_Null()
        {
            // Arrange.
            var systemSettingsSetter = new SystemSettingsSetter();
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();
            var managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection();
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.System);
            await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.System);
            var context = await _testContext.ProvisioningTestContext.CreateDataContext(accountName, password, spaceName);

            // Act.
            var act = new Action(() =>
            {
                systemSettingsSetter.Set(context, null);
            });

            // Assert.
            ExceptionAssert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public async Task SystemSettings_SystemSettingsSetter_Set_02()
        {
            // Arrange.
            var systemSettingsSetter = new SystemSettingsSetter();
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();
            var managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection();
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.System);
            await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.System);
            var context = await _testContext.ProvisioningTestContext.CreateDataContext(accountName, password, spaceName);
            var systemSettings = TestSystemSettings.Create();

            // Act.
            systemSettingsSetter.Set(context, systemSettings);

            // Assert.
            var processingResult = await context.Scripts.Process("<= /Providers/Google/PeopleApi");
            dynamic result = await processingResult.Output.LastOrDefaultAsync();
            TestSystemSettings.AreEqual(systemSettings, result);
        }


        [Fact]
        public async Task SystemSettings_SystemSettingsSetter_Set_03_Twice()
        {
            // Arrange.
            var systemSettingsSetter = new SystemSettingsSetter();
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();
            var managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection();
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.System);
            await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.System);
            var context = await _testContext.ProvisioningTestContext.CreateDataContext(accountName, password, spaceName);
            var firstSystemSettings = TestSystemSettings.Create();
            var secondSystemSettings = TestSystemSettings.Create();

            // Act.
            systemSettingsSetter.Set(context, firstSystemSettings);
            var processingResult = await context.Scripts.Process("<= /Providers/Google/PeopleApi");
            dynamic firstResult = await processingResult.Output.LastOrDefaultAsync();
            systemSettingsSetter.Set(context, secondSystemSettings);
            processingResult = await context.Scripts.Process("<= /Providers/Google/PeopleApi");
            dynamic secondResult = await processingResult.Output.LastOrDefaultAsync();


            // Assert.
            TestSystemSettings.AreEqual(firstSystemSettings, firstResult);
            TestSystemSettings.AreEqual(secondSystemSettings, secondResult);
        }


        [Fact]
        public async Task SystemSettings_SystemSettingsSetter_Set_03_Three_Times_Two_DataContext()
        {
            // Arrange.
            var systemSettingsSetter = new SystemSettingsSetter();
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();
            var managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection();
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.System);
            await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.System);
            var context = await _testContext.ProvisioningTestContext.CreateDataContext(accountName, password, spaceName);
            context.Dispose();
            var firstSystemSettings = TestSystemSettings.Create();
            var secondSystemSettings = TestSystemSettings.Create();
            var thirdSystemSettings = TestSystemSettings.Create();

            // Act.
            managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection();
            var connection = await managementConnection.OpenSpace(accountName, SpaceName.System);
            context = new DataContextFactory().Create(connection);

            systemSettingsSetter.Set(context, firstSystemSettings);
            var processingResult = await context.Scripts.Process("<= /Providers/Google/PeopleApi");
            dynamic firstResult = await processingResult.Output.LastOrDefaultAsync();
            context.Dispose();

            managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection();
            connection = await managementConnection.OpenSpace(accountName, SpaceName.System);
            context = new DataContextFactory().Create(connection);

            systemSettingsSetter.Set(context, secondSystemSettings);
            processingResult = await context.Scripts.Process("<= /Providers/Google/PeopleApi");
            dynamic secondResult = await processingResult.Output.LastOrDefaultAsync();
            context.Dispose();

            managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection();
            connection = await managementConnection.OpenSpace(accountName, SpaceName.System);
            context = new DataContextFactory().Create(connection);

            systemSettingsSetter.Set(context, thirdSystemSettings);
            processingResult = await context.Scripts.Process("<= /Providers/Google/PeopleApi");
            dynamic thirdResult = await processingResult.Output.LastOrDefaultAsync();
            context.Dispose();


            // Assert.
            TestSystemSettings.AreEqual(firstSystemSettings, firstResult);
            TestSystemSettings.AreEqual(secondSystemSettings, secondResult);
            TestSystemSettings.AreEqual(thirdSystemSettings, thirdResult);
        }
    }
}
