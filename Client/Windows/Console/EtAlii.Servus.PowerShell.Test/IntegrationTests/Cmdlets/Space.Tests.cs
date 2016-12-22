﻿namespace EtAlii.Servus.PowerShell.IntegrationTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.Servus.PowerShell.Tests;
    using Xunit;
    using System;
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Management;
    using TestAssembly = EtAlii.Servus.PowerShell.Tests.TestAssembly;

    
    public class Space_Test : IDisposable
    {
        private PowerShellTestContext _testContext;


        public Space_Test()
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
        }

        private void TestCleanup()
        {
            _testContext.Stop();
            _testContext = null;
        }

        [Fact]
        public void PowerShell_Spaces_Get()
        {
            var result = _testContext.InvokeGetSpaces();
            var spaces = _testContext.ToAssertedResult<List<Space>>(result);
        }

        [Fact]
        public void PowerShell_Space_Add()
        {
            var result = _testContext.InvokeGetSpaces();
            var spaces = _testContext.ToAssertedResult<List<Space>>(result);
            var firstCount = spaces.Count;

            var name = Guid.NewGuid().ToString();
            _testContext.InvokeAddSpace(name, SpaceTemplate.Data);

            result = _testContext.InvokeGetSpaces();
            spaces = _testContext.ToAssertedResult<List<Space>>(result);
            var secondCount = spaces.Count;

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
            Assert.Equal(root.Name, rootName);
        }

        private void Check_Root_Entry(string rootName)
        {
            var result = _testContext.InvokeGetRootByName(rootName);
            var root = _testContext.ToAssertedResult<Root>(result);
            Assert.NotNull(root);
            Assert.Equal(root.Name, rootName);
            //result = Invokeget
        }

        [Fact]
        public void PowerShell_Space_Update()
        {
            var result = _testContext.InvokeGetSpaces();

            var firstName = Guid.NewGuid().ToString();
            _testContext.InvokeAddSpace(firstName, SpaceTemplate.Data);

            result = _testContext.InvokeGetSpaceByName(firstName);
            var space = _testContext.ToAssertedResult<Space>(result);

            Assert.Equal(space.Name, firstName);

            var secondName = Guid.NewGuid().ToString();

            space.Name = secondName;

            _testContext.InvokeUpdateSpace(space);

            Exception exceptedException = null;
            try
            {
                result = _testContext.InvokeGetSpaceByName(firstName);
            }
            catch (Exception e)
            {
                exceptedException = e;
            }
            Assert.NotNull(exceptedException);

            result = _testContext.InvokeGetSpaceByName(secondName);
            space = _testContext.ToAssertedResult<Space>(result);

            Assert.Equal(space.Name, secondName);
        }


        [Fact]
        public void PowerShell_Space_Get_By_Name()
        {
            var name = Guid.NewGuid().ToString();
            _testContext.InvokeAddSpace(name, SpaceTemplate.Data);

            var result = _testContext.InvokeGetSpaceByName(name);
            var space = _testContext.ToAssertedResult<Space>(result);

            Assert.Equal(space.Name, name);
        }

        [Fact]
        public void PowerShell_Space_Get_By_Instance()
        {
            var name = Guid.NewGuid().ToString();
            _testContext.InvokeAddSpace(name, SpaceTemplate.Data);

            _testContext.InvokeSelectSpaceByName(name);

            var result = _testContext.InvokeGetSpaceByInstance();
            var space = _testContext.ToAssertedResult<Space>(result);

            Assert.Equal(space.Name, name);
        }


        [Fact]
        public void PowerShell_Space_Remove_By_Name()
        {
            var name = Guid.NewGuid().ToString();

            _testContext.InvokeAddSpace(name, SpaceTemplate.Data);

            var result = _testContext.InvokeGetSpaceByName(name);
            var space = _testContext.ToAssertedResult<Space>(result);

            Assert.Equal(space.Name, name);

            _testContext.InvokeRemoveSpaceByName(name);

            Exception exceptedException = null;
            try
            {
                result = _testContext.InvokeGetSpaceByName(name);
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

            Assert.Equal(space.Name, name);
        }

    }
}
