namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical;
    using EtAlii.Servus.Api.Logical.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;


    
    public class ScriptProcessor_RootedPath_Time_Regex_Separated_NoQuotes_Tests : IDisposable
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private static ILogicalTestContext _testContext;

        public ScriptProcessor_RootedPath_Time_Regex_Separated_NoQuotes_Tests()
        {
            var task = Task.Run(async () =>
            {
                _testContext = new LogicalTestContextFactory().Create();
                await _testContext.Start();

                _diagnostics = TestDiagnostics.Create();
                var scriptParserConfiguration = new ScriptParserConfiguration()
                    .Use(_diagnostics);
                _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
            });
            task.Wait();
        }

        public void Dispose()
        {
            var task = Task.Run(async () =>
            {
                _parser = null;

                await _testContext.Stop();
                _testContext = null;
            });
            task.Wait();
        }

        [Fact]
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYY_Regex_Separated_NoQuotes()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "time:2016",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/01/01/00/00/00/000";
            var selectQuery2 = "time:2016";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript1 = _parser.Parse(selectQuery1).Script;
            var selectScript2 = _parser.Parse(selectQuery2).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            var addResult = await lastSequence.Output.Cast<INode>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript1);
            var firstResult = await lastSequence.Output.Cast<INode>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript2);
            var secondResult = await lastSequence.Output.Cast<INode>().SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(firstResult);
            Assert.NotNull(secondResult);
            Assert.Equal(addResult.Id, firstResult.Id);
            Assert.Equal(addResult.Id, secondResult.Id);
        }
    }
}