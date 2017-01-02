namespace EtAlii.Servus.Api.Win32.Tests
{
    using System;
    using EtAlii.Servus.Api.Tests;
    using Infrastructure;
    using Infrastructure.Hosting.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Storage;

    public abstract class TestBase : EtAlii.Servus.Api.Tests.TestBase
    {
        public static void StartHosting(bool addAccountAndSpace)
        {
            StartHosting(TestAssembly.StorageName, addAccountAndSpace);
        }

        public static void StartHosting(string hostingName, bool addAccountAndSpace)
        {
            EtAlii.Servus.Api.Tests.TestBase.StartHosting(CreateHost, hostingName, CreateAddress, addAccountAndSpace);
        }
    }
}
