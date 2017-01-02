namespace EtAlii.Servus.Api.Data.IntegrationTests
{
    using System;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EtAlii.Servus.Api.Tests;

    [TestClass]
    public partial class DataContext_Nodes_Tests : EtAlii.Servus.Api.Data.Tests.TestBase
    {
        private IDiagnosticsConfiguration _diagnostics;

        [TestInitialize]
        public override void Initialize()
        {
            var start = Environment.TickCount;

            base.Initialize();
            _diagnostics = ApiTestHelper.CreateDiagnostics();

            Console.WriteLine("DataContext_Nodes.Initialize: {0}ms", Environment.TickCount - start);
        }

        [TestCleanup]
        public override void Cleanup()
        {
            var start = Environment.TickCount;

            base.Cleanup();
            _diagnostics = null;

            Console.WriteLine("DataContext_Nodes.Cleanup: {0}ms", Environment.TickCount - start);
        }
    }
}
