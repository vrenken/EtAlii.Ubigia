namespace EtAlii.Ubigia.PowerShell.Tests
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using System;
    using System.Collections.Generic;
    using System.Management.Automation;
    using Xunit;


    public class RootTest : IDisposable
    {
        private PowerShellTestContext _testContext;

        public RootTest()
        {
            TestInitialize();
        }

        public void Dispose()
        {
            TestCleanup();
        }

        private void TestInitialize()
        {
            _testContext = new PowerShellTestContext();
            _testContext.Start();

            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            _testContext.InvokeAddAccount(accountName, password, AccountTemplate.User);
            _testContext.InvokeSelectAccountByName(accountName);

            var name = Guid.NewGuid().ToString();

            _testContext.InvokeAddSpace(name, SpaceTemplate.Data);
            var result = _testContext.InvokeSelectSpaceByName(name);
            _testContext.ToAssertedResult<Space>(result);
        }

        private void TestCleanup()
        {
            _testContext.Stop();
            _testContext = null;
        }

        [Fact]
        public void PowerShell_Root_Add_And_Get_WithInitializeAndCleanup()
        {
            PowerShell_Root_Add();

            TestCleanup();
            TestInitialize();

            PowerShell_Roots_Get();
        }

        [Fact]
        public void PowerShell_Roots_Get()
        {
            // Arrange.

            // Act.
            var result = _testContext.InvokeGetRoots();
            var roots = _testContext.ToAssertedResult<List<Root>>(result);

            // Assert.
            Assert.NotEmpty(roots);
        }

        [Fact]
        public void PowerShell_Root_Add()
        {
            // Arrange.
            var result = _testContext.InvokeGetRoots();
            var roots = _testContext.ToAssertedResult<List<Root>>(result);
            var firstCount = roots.Count;
            var name = Guid.NewGuid().ToString();

            // Act.
            _testContext.InvokeAddRoot(name);

            // Assert.
            result = _testContext.InvokeGetRoots();
            roots = _testContext.ToAssertedResult<List<Root>>(result);
            var secondCount = roots.Count;

            Assert.True(secondCount == firstCount + 1);
        }

        [Fact]
        public void PowerShell_Root_Update()
        {
            // Arrange.
            //var result = _testContext.InvokeGetRoots();
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
            var name = Guid.NewGuid().ToString();

            _testContext.InvokeAddRoot(name);

            var result = _testContext.InvokeGetRootByName(name);
            var root = _testContext.ToAssertedResult<Root>(result);

            Assert.Equal(name, root.Name);

            _testContext.InvokeRemoveRootByName(name);

            Exception exceptedException = null;
            try
            {
                _testContext.InvokeGetRootByName(name);
            }
            catch (Exception e)
            {
                exceptedException = e;
            }
            Assert.NotNull(exceptedException);
        }


        [Fact]
        public void PowerShell_Root_Remove_By_Instance()
        {
            var name = Guid.NewGuid().ToString();
            _testContext.InvokeAddRoot(name);

            _testContext.InvokeSelectRootByName(name);

            _testContext.InvokeRemoveRootByInstance();

            Exception exceptedException = null;
            try
            {
                _testContext.InvokeGetRootByName(name);
            }
            catch (Exception e)
            {
                exceptedException = e;
            }
            Assert.NotNull(exceptedException);
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
