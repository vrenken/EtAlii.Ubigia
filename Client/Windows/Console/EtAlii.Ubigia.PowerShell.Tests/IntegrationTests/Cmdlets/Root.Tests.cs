namespace EtAlii.Ubigia.PowerShell.Tests
{
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using Xunit;

    public class RootTest : IAsyncLifetime
    {
        private PowerShellTestContext _testContext;

        public async Task InitializeAsync()
        {
            _testContext = new PowerShellTestContext();
            await _testContext.Start();

            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            _testContext.InvokeAddAccount(accountName, password, AccountTemplate.User);
            _testContext.InvokeSelectAccountByName(accountName);

            var name = Guid.NewGuid().ToString();

            _testContext.InvokeAddSpace(name, SpaceTemplate.Data);
            var result = _testContext.InvokeSelectSpaceByName(name);
            _testContext.ToAssertedResult<Space>(result);
        }

        public async Task DisposeAsync()
        {
            await _testContext.Stop();
            _testContext = null;
        }

        [Fact]
        public async Task PowerShell_Root_Add_And_Get_WithInitializeAndCleanup()
        {
            PowerShell_Root_Add();

            await DisposeAsync();
            await InitializeAsync();

            PowerShell_Roots_Get();
        }

        [Fact]
        public void PowerShell_Roots_Get()
        {
            // Arrange.

            // Act.
            var result = _testContext.InvokeGetRoots();
            var roots = _testContext.ToAssertedResults<Root>(result);

            // Assert.
            Assert.NotEmpty(roots);
        }

        [Fact]
        public void PowerShell_Root_Add()
        {
            // Arrange.
            var result = _testContext.InvokeGetRoots();
            var roots = _testContext.ToAssertedResults<Root>(result);
            var firstCount = roots.Length;
            var name = Guid.NewGuid().ToString();

            // Act.
            _testContext.InvokeAddRoot(name);

            // Assert.
            result = _testContext.InvokeGetRoots();
            roots = _testContext.ToAssertedResults<Root>(result);
            var secondCount = roots.Length;

            Assert.True(secondCount == firstCount + 1);
        }

        [Fact]
        public void PowerShell_Root_Update()
        {
            // Arrange.
            //var result = _testContext.InvokeGetRoots()
            var firstName = Guid.NewGuid().ToString();
            _testContext.InvokeAddRoot(firstName);
            var result = _testContext.InvokeGetRootByName(firstName);
            var originalRoot = _testContext.ToAssertedResult<Root>(result);
            result = _testContext.InvokeGetRootByName(firstName);
            var updatedRoot = _testContext.ToAssertedResult<Root>(result);
            var secondName = Guid.NewGuid().ToString();
            updatedRoot.Name = secondName;

            // Act.
            _testContext.InvokeUpdateRoot(updatedRoot);
            Assert.Throws<CmdletInvocationException>(() => { result = _testContext.InvokeGetRootByName(firstName); });
            result = _testContext.InvokeGetRootByName(secondName);
            updatedRoot = _testContext.ToAssertedResult<Root>(result);

            // Assert.
            Assert.Equal(firstName, originalRoot.Name);
            Assert.Equal(secondName, updatedRoot.Name);
        }


        [Fact]
        public void PowerShell_Root_Get_By_Name()
        {
            var name = Guid.NewGuid().ToString();
            var result = _testContext.InvokeAddRoot(name);

            var root = _testContext.ToAssertedResult<Root>(result);
            Assert.Equal(name, root.Name);
            Assert.NotEqual(Guid.Empty, root.Id);

            result = _testContext.InvokeGetRootByName(name);
            var rootRetrieved = _testContext.ToAssertedResult<Root>(result);

            Assert.Equal(root.Name, rootRetrieved.Name);
            Assert.Equal(root.Id, rootRetrieved.Id);
        }

        [Fact]
        public void PowerShell_Root_Get_By_Id()
        {
            var name = Guid.NewGuid().ToString();
            var result = _testContext.InvokeAddRoot(name);

            var root = _testContext.ToAssertedResult<Root>(result);
            Assert.Equal(name, root.Name);
            Assert.NotEqual(Guid.Empty, root.Id);

            result = _testContext.InvokeGetRootById(root.Id);
            var rootRetrieved = _testContext.ToAssertedResult<Root>(result);

            Assert.Equal(root.Name, rootRetrieved.Name);
            Assert.Equal(root.Id, rootRetrieved.Id);
        }

        [Fact]
        public void PowerShell_Root_Get_By_Instance()
        {
            var name = Guid.NewGuid().ToString();
            _testContext.InvokeAddRoot(name);

            _testContext.InvokeSelectRootByName(name);

            var result = _testContext.InvokeGetRootByInstance();
            var root = _testContext.ToAssertedResult<Root>(result);

            Assert.Equal(name, root.Name);
        }


        [Fact]
        public void PowerShell_Root_Remove_By_Name()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            _testContext.InvokeAddRoot(name);
            var result = _testContext.InvokeGetRootByName(name);
            var root = _testContext.ToAssertedResult<Root>(result);
            Assert.Equal(name, root.Name);
            _testContext.InvokeRemoveRootByName(name);

            // Act.
            var act = new Action(() => _testContext.InvokeGetRootByName(name));

            // Assert.
            Assert.Throws<CmdletInvocationException>(act);
        }


        [Fact]
        public void PowerShell_Root_Remove_By_Instance()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            _testContext.InvokeAddRoot(name);
            _testContext.InvokeSelectRootByName(name);
            _testContext.InvokeRemoveRootByInstance();

            // Act.
            var act = new Action(() => _testContext.InvokeGetRootByName(name));
            
            // Assert.
            Assert.Throws<CmdletInvocationException>(act);
        }

        [Fact]
        public void PowerShell_Root_Select()
        {
            var name = Guid.NewGuid().ToString();
            _testContext.InvokeAddRoot(name);

            var result = _testContext.InvokeSelectRootByName(name);
            var root = _testContext.ToAssertedResult<Root>(result);
             
            Assert.Equal(name, root.Name);
        }
    }
}
