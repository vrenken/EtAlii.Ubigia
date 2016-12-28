﻿namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical;
    using EtAlii.Servus.Api.Logical.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;


    
    public class ScriptProcessor_RootedPath_Time_Regex_Separated_SingleQuotes_Tests : IDisposable
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private static ILogicalTestContext _testContext;

        public ScriptProcessor_RootedPath_Time_Regex_Separated_SingleQuotes_Tests()
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
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYY_Regex_Separated_SingleQuotes()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "time:'2016'",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/01/01/00/00/00/000";
            var selectQuery2 = "time:'2016'";

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

        [Fact]
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYYMM_Regex_Separated_SingleQuotes()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);

            var addQueries = new[]
            {
                "time:'2016-09'",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/09/01/00/00/00/000";
            var selectQuery2 = "time:'2016-09'";

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

        [Fact]
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYYMMDD_Regex_Separated_SingleQuotes()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);

            var addQueries = new[]
            {
                "time:'2016-09-01'",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/09/01/00/00/00/000";
            var selectQuery2 = "time:'2016-09-01'";

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

        [Fact]
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYYMMDDHH_Regex_Separated_SingleQuotes()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);

            var addQueries = new[]
            {
                "time:'2016-09-01 22'",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/09/01/22/00/00/000";
            var selectQuery2 = "time:'2016-09-01 22'";

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

        [Fact]
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYYMMDDHHMM_Regex_Separated_SingleQuotes()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);

            var addQueries = new[]
            {
                "time:'2016-09-01 22:05'",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/09/01/22/05/00/000";
            var selectQuery2 = "time:'2016-09-01 22:05'";

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

        [Fact]
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYYMMDDHHMMSS_Regex_Separated_SingleQuotes()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);

            var addQueries = new[]
            {
                "time:'2016-09-01 22:05:23'",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/09/01/22/05/23/000";
            var selectQuery2 = "time:'2016-09-01 22:05:23'";

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

        [Fact]
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYYMMDDHHMMSSMMM_Regex_Separated_SingleQuotes()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);

            var addQueries = new[]
            {
                "time:'2016-09-01 22:05:23.123'",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/09/01/22/05/23/123";
            var selectQuery2 = "time:'2016-09-01 22:05:23.123'";

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