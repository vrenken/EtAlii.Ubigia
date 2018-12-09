namespace EtAlii.Ubigia.Provisioning.Google.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Provisioning.Google.PeopleApi;
    using EtAlii.Ubigia.Provisioning.Tests;
    using Xunit;

    
    public class SystemSettingsTests : IClassFixture<ProvisioningUnitTestContext>
    {
        private readonly ProvisioningUnitTestContext _testContext;

        public SystemSettingsTests(ProvisioningUnitTestContext testContext)
        {
            _testContext = testContext;
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
            var context = await _testContext.ProvisioningTestContext.CreateScriptContext(accountName, password, spaceName);

            // Act.
            var act = new Action(() =>
            {
                systemSettingsSetter.Set(context, null);
            });

            // Assert.
            Assert.Throws<ArgumentNullException>(act);
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
            var context = await _testContext.ProvisioningTestContext.CreateScriptContext(accountName, password, spaceName);
            var systemSettings = TestSystemSettings.Create();

            // Act.
            systemSettingsSetter.Set(context, systemSettings);

            // Assert.
            var processingResult = await context.Process("<= /Providers/Google/PeopleApi");
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
            var context = await _testContext.ProvisioningTestContext.CreateScriptContext(accountName, password, spaceName);
            var firstSystemSettings = TestSystemSettings.Create();
            var secondSystemSettings = TestSystemSettings.Create();

            // Act.
            systemSettingsSetter.Set(context, firstSystemSettings);
            var processingResult = await context.Process("<= /Providers/Google/PeopleApi");
            dynamic firstResult = await processingResult.Output.LastOrDefaultAsync();
            systemSettingsSetter.Set(context, secondSystemSettings);
            processingResult = await context.Process("<= /Providers/Google/PeopleApi");
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
            var context = await _testContext.ProvisioningTestContext.CreateScriptContext(accountName, password, spaceName);
            
            var firstSystemSettings = TestSystemSettings.Create();
            var secondSystemSettings = TestSystemSettings.Create();
            var thirdSystemSettings = TestSystemSettings.Create();

            // Act.
            managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection();
            var connection = await managementConnection.OpenSpace(accountName, SpaceName.System);
            context = new DataContextFactory().Create(connection).CreateGraphSLScriptContext();

            systemSettingsSetter.Set(context, firstSystemSettings);
            var processingResult = await context.Process("<= /Providers/Google/PeopleApi");
            dynamic firstResult = await processingResult.Output.LastOrDefaultAsync();
            
            managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection();
            connection = await managementConnection.OpenSpace(accountName, SpaceName.System);
            context = new DataContextFactory().Create(connection).CreateGraphSLScriptContext();

            systemSettingsSetter.Set(context, secondSystemSettings);
            processingResult = await context.Process("<= /Providers/Google/PeopleApi");
            dynamic secondResult = await processingResult.Output.LastOrDefaultAsync();
            
            managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection();
            connection = await managementConnection.OpenSpace(accountName, SpaceName.System);
            context = new DataContextFactory().Create(connection).CreateGraphSLScriptContext();

            systemSettingsSetter.Set(context, thirdSystemSettings);
            processingResult = await context.Process("<= /Providers/Google/PeopleApi");
            dynamic thirdResult = await processingResult.Output.LastOrDefaultAsync();
            
            // Assert.
            TestSystemSettings.AreEqual(firstSystemSettings, firstResult);
            TestSystemSettings.AreEqual(secondSystemSettings, secondResult);
            TestSystemSettings.AreEqual(thirdSystemSettings, thirdResult);
        }
    }
}
