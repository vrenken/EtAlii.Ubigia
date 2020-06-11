namespace EtAlii.Ubigia.PowerShell.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using Xunit;

    public class SpaceTest : IAsyncLifetime
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
        }

        public async Task DisposeAsync()
        {
            await _testContext.Stop();
            _testContext = null;
        }

        [Fact]
        public void PowerShell_Spaces_Get()
        {
            // Arrange.
            
            // Act.
            var result = _testContext.InvokeGetSpaces();
            var spaces = _testContext.ToAssertedResults<Space>(result);
            
            // Assert.
            Assert.NotEmpty(spaces);
        }

        [Fact]
        public void PowerShell_Space_Add()
        {
            // Arrange.
            var result = _testContext.InvokeGetSpaces();
            var spaces = _testContext.ToAssertedResults<Space>(result);
            var firstCount = spaces.Length;

            // Act.
            var name = Guid.NewGuid().ToString();
            _testContext.InvokeAddSpace(name, SpaceTemplate.Data);
             
            
            // Assert.
            result = _testContext.InvokeGetSpaces();
            spaces = _testContext.ToAssertedResults<Space>(result);
            var secondCount = spaces.Length;
            Assert.True(secondCount == firstCount + 1);
        }

        [Fact]
        public void PowerShell_Space_Add_Check_Roots()
        {
            var name = Guid.NewGuid().ToString();
            _testContext.InvokeAddSpace(name, SpaceTemplate.Data);
            _testContext.InvokeSelectSpaceByName(name);

            Check_Root("Tail");
            Check_Root("Head");
        }

        [Fact]
        public void PowerShell_Space_Add_Check_Root_Entries()
        {
            var name = Guid.NewGuid().ToString();
            _testContext.InvokeAddSpace(name, SpaceTemplate.Data);
            _testContext.InvokeSelectSpaceByName(name);

            Check_Root_Entry("Tail");
            Check_Root_Entry("Head");
        }

        private void Check_Root(string rootName)
        {
            var result = _testContext.InvokeGetRootByName(rootName);
            var root = _testContext.ToAssertedResult<Root>(result);
            Assert.NotNull(root);
            Assert.Equal(rootName, root.Name);
        }

        private void Check_Root_Entry(string rootName)
        {
            var result = _testContext.InvokeGetRootByName(rootName);
            var root = _testContext.ToAssertedResult<Root>(result);
            Assert.NotNull(root);
            Assert.Equal(rootName, root.Name);
            //result = Invokeget
        }

        [Fact]
        public void PowerShell_Space_Update()
        {
            _testContext.InvokeGetSpaces();

            var firstName = Guid.NewGuid().ToString();
            _testContext.InvokeAddSpace(firstName, SpaceTemplate.Data);

            var result = _testContext.InvokeGetSpaceByName(firstName);
            var space = _testContext.ToAssertedResult<Space>(result);

            Assert.Equal(firstName, space.Name);

            var secondName = Guid.NewGuid().ToString();

            space.Name = secondName;

            _testContext.InvokeUpdateSpace(space);

            Exception exceptedException = null;
            try
            {
                _testContext.InvokeGetSpaceByName(firstName);
            }
            catch (Exception e)
            {
                exceptedException = e;
            }
            Assert.NotNull(exceptedException);

            result = _testContext.InvokeGetSpaceByName(secondName);
            space = _testContext.ToAssertedResult<Space>(result);

            Assert.Equal(secondName, space.Name);
        }


        [Fact]
        public void PowerShell_Space_Get_By_Name()
        {
            var name = Guid.NewGuid().ToString();
            _testContext.InvokeAddSpace(name, SpaceTemplate.Data);

            var result = _testContext.InvokeGetSpaceByName(name);
            var space = _testContext.ToAssertedResult<Space>(result);

            Assert.Equal(name, space.Name);
        }

        [Fact]
        public void PowerShell_Space_Get_By_Instance()
        {
            var name = Guid.NewGuid().ToString();
            _testContext.InvokeAddSpace(name, SpaceTemplate.Data);

            _testContext.InvokeSelectSpaceByName(name);

            var result = _testContext.InvokeGetSpaceByInstance();
            var space = _testContext.ToAssertedResult<Space>(result);

            Assert.Equal(name, space.Name);
        }


        [Fact]
        public void PowerShell_Space_Remove_By_Name()
        {
            var name = Guid.NewGuid().ToString();

            _testContext.InvokeAddSpace(name, SpaceTemplate.Data);

            var result = _testContext.InvokeGetSpaceByName(name);
            var space = _testContext.ToAssertedResult<Space>(result);

            Assert.Equal(name, space.Name);

            _testContext.InvokeRemoveSpaceByName(name);

            Exception exceptedException = null;
            try
            {
                _testContext.InvokeGetSpaceByName(name);
            }
            catch (Exception e)
            {
                exceptedException = e;
            }
            Assert.NotNull(exceptedException);
        }


        [Fact]
        public void PowerShell_Space_Remove_By_Instance()
        {
            var name = Guid.NewGuid().ToString();
            _testContext.InvokeAddSpace(name, SpaceTemplate.Data);

            _testContext.InvokeSelectSpaceByName(name);

            _testContext.InvokeRemoveSpaceByInstance();

            Exception exceptedException = null;
            try
            {
                _testContext.InvokeGetSpaceByName(name);
            }
            catch (Exception e)
            {
                exceptedException = e;
            }
            Assert.NotNull(exceptedException);
        }

        [Fact]
        public void PowerShell_Space_Select()
        {
            var name = Guid.NewGuid().ToString();
            _testContext.InvokeAddSpace(name, SpaceTemplate.Data);

            var result = _testContext.InvokeSelectSpaceByName(name);
            var space = _testContext.ToAssertedResult<Space>(result);

            Assert.Equal(name, space.Name);
        }

    }
}
