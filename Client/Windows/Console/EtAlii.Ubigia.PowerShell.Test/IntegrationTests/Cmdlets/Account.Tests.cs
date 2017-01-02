namespace EtAlii.Ubigia.PowerShell.IntegrationTests
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.PowerShell.Tests;
    using Xunit;
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api.Management;
    using EtAlii.Ubigia.Infrastructure.Hosting;
    using TestAssembly = EtAlii.Ubigia.PowerShell.Tests.TestAssembly;

    
    public class Account_Test : IDisposable
    {
        private PowerShellTestContext _testContext;

        public Account_Test()
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
        }
        private void TestCleanup()
        {
            _testContext.Stop();
            _testContext = null;
        }

        [Fact]
        public void PowerShell_Accounts_Get()
        {
            // Arrange.

            // Act.
            var result = _testContext.InvokeGetAccounts();

            // Assert.
            var Accounts = _testContext.ToAssertedResult<List<Account>>(result);
        }

        [Fact]
        public void PowerShell_Account_Add_And_Get_WithInitializeAndCleanup()
        {
            PowerShell_Account_Add();

            TestCleanup();
            TestInitialize();

            PowerShell_Accounts_Get();
        }

        [Fact]
        public void PowerShell_Account_Add()
        {
            // Arrange.
            var result = _testContext.InvokeGetAccounts();
            var accounts = _testContext.ToAssertedResult<List<Account>>(result);
            var firstCount = accounts.Count;
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            // Act.
            _testContext.InvokeAddAccount(accountName, password, AccountTemplate.User);

            // Assert.
            result = _testContext.InvokeGetAccounts();
            accounts = _testContext.ToAssertedResult<List<Account>>(result);
            var secondCount = accounts.Count;
            Assert.True(secondCount == firstCount + 1);
        }

        [Fact]
        public void PowerShell_Account_Update()
        {
            var result = _testContext.InvokeGetAccounts();

            var firstAccountName = Guid.NewGuid().ToString();
            var firstPassword = Guid.NewGuid().ToString();
            _testContext.InvokeAddAccount(firstAccountName, firstPassword, AccountTemplate.User);

            result = _testContext.InvokeGetAccountByName(firstAccountName);
            var account = _testContext.ToAssertedResult<Account>(result);

            Assert.Equal(account.Name, firstAccountName);
            Assert.Equal(account.Password, firstPassword);

            var secondAccountName = Guid.NewGuid().ToString();
            var secondPassword = Guid.NewGuid().ToString();

            account.Name = secondAccountName;
            account.Password = secondPassword;

            _testContext.InvokeUpdateAccount(account);

            Exception exceptedException = null;
            try
            {
                result = _testContext.InvokeGetAccountByName(firstAccountName);
            }
            catch (Exception e)
            {
                exceptedException = e;
            }
            Assert.NotNull(exceptedException);

            result = _testContext.InvokeGetAccountByName(secondAccountName);
            account = _testContext.ToAssertedResult<Account>(result);

            Assert.Equal(account.Name, secondAccountName);
            Assert.Equal(account.Password, secondPassword);
        }

        [Fact]
        public void PowerShell_Account_Get_By_Name()
        {
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            _testContext.InvokeAddAccount(accountName, password, AccountTemplate.User);

            var result = _testContext.InvokeGetAccountByName(accountName);
            var account = _testContext.ToAssertedResult<Account>(result);

            Assert.Equal(account.Name, accountName);
        }

        [Fact]
        public void PowerShell_Account_Get_By_Instance()
        {
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            _testContext.InvokeAddAccount(accountName, password, AccountTemplate.User);

            _testContext.InvokeSelectAccountByName(accountName);

            var result = _testContext.InvokeGetAccountByInstance();
            var account = _testContext.ToAssertedResult<Account>(result);

            Assert.Equal(account.Name, accountName);
        }

        [Fact]
        public void PowerShell_Account_Remove_By_Name()
        {
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            _testContext.InvokeAddAccount(accountName, password, AccountTemplate.User);

            var result = _testContext.InvokeGetAccountByName(accountName);
            var account = _testContext.ToAssertedResult<Account>(result);

            Assert.Equal(account.Name, accountName);

            _testContext.InvokeRemoveAccountByName(accountName);

            Exception exceptedException = null;
            try
            {
                result = _testContext.InvokeGetAccountByName(accountName);
            }
            catch (Exception e)
            {
                exceptedException = e;
            }
            Assert.NotNull(exceptedException);
        }

        [Fact]
        public void PowerShell_Account_Remove_By_Instance()
        {
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            _testContext.InvokeAddAccount(accountName, password, AccountTemplate.User);

            _testContext.InvokeSelectAccountByName(accountName);

            _testContext.InvokeRemoveAccountByInstance();

            Exception exceptedException = null;
            try
            {
                _testContext.InvokeGetAccountByName(accountName);
            }
            catch (Exception e)
            {
                exceptedException = e;
            }
            Assert.NotNull(exceptedException);
        }

        [Fact]
        public void PowerShell_Account_Select()
        {
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            _testContext.InvokeAddAccount(accountName, password, AccountTemplate.User);

            var result = _testContext.InvokeSelectAccountByName(accountName);
            var account = _testContext.ToAssertedResult<Account>(result);

            Assert.Equal(account.Name, accountName);
        }
    }
}
