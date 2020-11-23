namespace EtAlii.Ubigia.Provisioning.Google.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.Ubigia.Api.Logical;
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
            var managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection().ConfigureAwait(false);
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.System).ConfigureAwait(false);
            await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.System).ConfigureAwait(false);
            var context = await _testContext.ProvisioningTestContext.CreateScriptContext(accountName, password, spaceName).ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async() =>
            {
                await systemSettingsSetter.Set(context, null).ConfigureAwait(false);
            });

            // Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(act).ConfigureAwait(false);
        }

        [Fact]
        public async Task SystemSettings_SystemSettingsSetter_Set_02()
        {
            // Arrange.
            var systemSettingsSetter = new SystemSettingsSetter();
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();
            var managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection().ConfigureAwait(false);
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.System).ConfigureAwait(false);
            await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.System).ConfigureAwait(false);
            var context = await _testContext.ProvisioningTestContext.CreateScriptContext(accountName, password, spaceName).ConfigureAwait(false);
            var systemSettings = TestSystemSettings.Create();

            // Act.
            await systemSettingsSetter.Set(context, systemSettings).ConfigureAwait(false);

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
            var managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection().ConfigureAwait(false);
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.System).ConfigureAwait(false);
            await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.System).ConfigureAwait(false);
            var context = await _testContext.ProvisioningTestContext.CreateScriptContext(accountName, password, spaceName).ConfigureAwait(false);
            var firstSystemSettings = TestSystemSettings.Create();
            var secondSystemSettings = TestSystemSettings.Create();

            // Act.
            await systemSettingsSetter.Set(context, firstSystemSettings).ConfigureAwait(false);
            var processingResult = await context.Process("<= /Providers/Google/PeopleApi");
            dynamic firstResult = await processingResult.Output.LastOrDefaultAsync();
            await systemSettingsSetter.Set(context, secondSystemSettings).ConfigureAwait(false);
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
            var managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection().ConfigureAwait(false);
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.System).ConfigureAwait(false);
            var addedSpace = await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.System).ConfigureAwait(false);
            //var context = await _testContext.ProvisioningTestContext.CreateScriptContext(accountName, password, spaceName)
            
            var firstSystemSettings = TestSystemSettings.Create();
            var secondSystemSettings = TestSystemSettings.Create();
            var thirdSystemSettings = TestSystemSettings.Create();

            // Act.
            managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection().ConfigureAwait(false);
            var connection = await managementConnection.OpenSpace(accountName, spaceName).ConfigureAwait(false);
            var configuration = new GraphSLScriptContextConfiguration()
                .UseCaching(true)
                .UseTraversalCaching(true)
                .Use(connection);
            var context = new GraphSLScriptContextFactory().Create(configuration);

            await systemSettingsSetter.Set(context, firstSystemSettings).ConfigureAwait(false);
            var processingResult = await context.Process("<= /Providers/Google/PeopleApi");
            dynamic firstResult = await processingResult.Output.LastOrDefaultAsync();
            
            managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection().ConfigureAwait(false);
            connection = await managementConnection.OpenSpace(accountName, spaceName).ConfigureAwait(false);
            configuration = new GraphSLScriptContextConfiguration()
                .UseCaching(true)
                .UseTraversalCaching(true)
                .Use(connection);
            context = new GraphSLScriptContextFactory().Create(configuration);
            
            await systemSettingsSetter.Set(context, secondSystemSettings).ConfigureAwait(false);
            processingResult = await context.Process("<= /Providers/Google/PeopleApi");
            dynamic secondResult = await processingResult.Output.LastOrDefaultAsync();
            
            managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection().ConfigureAwait(false);
            connection = await managementConnection.OpenSpace(accountName, spaceName).ConfigureAwait(false);
            configuration = new GraphSLScriptContextConfiguration()
                .UseCaching(true)
                .UseTraversalCaching(true)
                .Use(connection);
            context = new GraphSLScriptContextFactory().Create(configuration);
            
            await systemSettingsSetter.Set(context, thirdSystemSettings).ConfigureAwait(false);
            processingResult = await context.Process("<= /Providers/Google/PeopleApi");
            dynamic thirdResult = await processingResult.Output.LastOrDefaultAsync();
            
            // Assert.
            Assert.NotNull(addedSpace);
            TestSystemSettings.AreEqual(firstSystemSettings, firstResult);
            TestSystemSettings.AreEqual(secondSystemSettings, secondResult);
            TestSystemSettings.AreEqual(thirdSystemSettings, thirdResult);
        }
        
        
        [Fact]
        public async Task SystemSettings_SystemSettingsSetter_Set_03_Three_Times_Two_DataContext_Disposed()
        {
            // Arrange.
            var systemSettingsSetter = new SystemSettingsSetter();
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();
            var managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection().ConfigureAwait(false);
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.System).ConfigureAwait(false);
            await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.System).ConfigureAwait(false);
            managementConnection.Dispose();

            var firstSystemSettings = TestSystemSettings.Create();
            var secondSystemSettings = TestSystemSettings.Create();
            var thirdSystemSettings = TestSystemSettings.Create();

            // Act.
            managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection().ConfigureAwait(false);
            var connection = await managementConnection.OpenSpace(accountName, spaceName).ConfigureAwait(false);
            var configuration = new GraphSLScriptContextConfiguration()
                .UseCaching(true)
                .UseTraversalCaching(true)
                .Use(connection);
            var context = new GraphSLScriptContextFactory().Create(configuration);

            await systemSettingsSetter.Set(context, firstSystemSettings).ConfigureAwait(false);
            var processingResult = await context.Process("<= /Providers/Google/PeopleApi");
            dynamic firstResult = await processingResult.Output.LastOrDefaultAsync();
            managementConnection.Dispose();
            
            managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection().ConfigureAwait(false);
            connection = await managementConnection.OpenSpace(accountName, spaceName).ConfigureAwait(false);
            configuration = new GraphSLScriptContextConfiguration()
                .UseCaching(true)
                .UseTraversalCaching(true)
                .Use(connection);
            context = new GraphSLScriptContextFactory().Create(configuration);
            
            await systemSettingsSetter.Set(context, secondSystemSettings).ConfigureAwait(false);
            processingResult = await context.Process("<= /Providers/Google/PeopleApi");
            dynamic secondResult = await processingResult.Output.LastOrDefaultAsync();
            managementConnection.Dispose();
            
            managementConnection = await _testContext.ProvisioningTestContext.OpenManagementConnection().ConfigureAwait(false);
            connection = await managementConnection.OpenSpace(accountName, spaceName).ConfigureAwait(false);
            configuration = new GraphSLScriptContextConfiguration()
                .UseCaching(true)
                .UseTraversalCaching(true)
                .Use(connection);
            context = new GraphSLScriptContextFactory().Create(configuration);
            
            await systemSettingsSetter.Set(context, thirdSystemSettings).ConfigureAwait(false);
            processingResult = await context.Process("<= /Providers/Google/PeopleApi");
            dynamic thirdResult = await processingResult.Output.LastOrDefaultAsync();
            managementConnection.Dispose();
            
            // Assert.
            TestSystemSettings.AreEqual(firstSystemSettings, firstResult);
            TestSystemSettings.AreEqual(secondSystemSettings, secondResult);
            TestSystemSettings.AreEqual(thirdSystemSettings, thirdResult);
        }
    }
}
