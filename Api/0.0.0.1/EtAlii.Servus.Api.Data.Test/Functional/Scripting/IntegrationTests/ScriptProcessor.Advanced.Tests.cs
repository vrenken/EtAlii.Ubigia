namespace EtAlii.Servus.Api.Data.IntegrationTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Data;
    using EtAlii.Servus.Api.Data.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Infrastructure.Hosting.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using TestAssembly = EtAlii.Servus.Api.Data.Tests.TestAssembly;


    [TestClass]
    public class ScriptProcessor_Advanced_Tests : EtAlii.Servus.Api.Data.Tests.TestBase
    {
        private IScriptProcessor _processor;
        private IScriptParser _parser;

        [ClassInitialize]
        public override void Initialize()
        {
            base.Initialize();

            var diagnostics = ApiTestHelper.CreateDiagnostics();
            _parser = new ScriptParserFactory().Create(diagnostics);
            _processor = new ScriptProcessorFactory().Create(diagnostics);
        }

        [ClassCleanup]
        public override void Cleanup()
        {
            _parser = null;
            _processor = null;

            base.Cleanup();
        }
    }
}