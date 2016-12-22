namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Diagnostics.Tests;
    using EtAlii.Servus.Api.Logical;
    using EtAlii.Servus.Api.Logical.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;
    using TestAssembly = EtAlii.Servus.Api.Tests.TestAssembly;

    
    public class ScriptProcessor_Scripted_Add_Tests : IClassFixture<LogicalUnitTestContext>, IDisposable
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;
        private readonly LogicalUnitTestContext _testContext;

        public ScriptProcessor_Scripted_Add_Tests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
            var task = Task.Run(async () =>
            {
                _diagnostics = TestDiagnostics.Create();
                var scriptParserConfiguration = new ScriptParserConfiguration()
                    .Use(_diagnostics);
                _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
                _logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            });
            task.Wait();
        }

        public void Dispose()
        {
            var task = Task.Run(() =>
            {
                _parser = null;
                _logicalContext.Dispose();
                _logicalContext = null;
            });
            task.Wait();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_Simple()
        {
            // Arrange.
            var now = DateTime.Now;
            var addQueries = new[]
            {
                "/Time+=/{0:yyyy}",
                "<= /Time/{0:yyyy}"
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var selectQuery = String.Format("<= /Time/{0:yyyy}", now);

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic firstYearEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic secondYearEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstYearEntry);
            Assert.NotNull(secondYearEntry);
            Assert.Equal(((INode)firstYearEntry).Id, ((INode)secondYearEntry).Id);
            Assert.Equal(String.Format("{0:yyyy}", now), firstYearEntry.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_1()
        {
            // Arrange.
            var now = DateTime.Now;
            var addQueries = new[]
            {
                "/Time+=/{0:yyyy}",
                "/Time/{0:yyyy}+=/{0:MM}",
                "/Time/{0:yyyy}/{0:MM}+=/{0:dd}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}+=/{0:HH}",
                "<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}+=/{0:mm}"
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var selectQuery = String.Format("<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic firstMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic secondMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstMinuteEntry);
            Assert.NotNull(secondMinuteEntry);
            Assert.Equal(((INode)firstMinuteEntry).Id, ((INode)secondMinuteEntry).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_2()
        {
            // Arrange.
            var now = DateTime.Now;
            var addQueries = new[]
            {
                "/Time+=/{0:yyyy}",
                "/Time/{0:yyyy}+=/{0:MM}",
                "/Time/{0:yyyy}/{0:MM}+=/{0:dd}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}+=/{0:HH}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}+=/{0:mm}",
                "<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var selectQuery = String.Format("<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic firstMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic secondMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstMinuteEntry);
            Assert.NotNull(secondMinuteEntry);
            Assert.Equal(((INode)firstMinuteEntry).Id, ((INode)secondMinuteEntry).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_3()
        {
            // Arrange.
            var now = DateTime.Now;
            var addQueries = new[]
            {
                "/Time+=/{0:yyyy}",
                "/Time/{0:yyyy}+=/{0:MM}",
                "/Time/{0:yyyy}/{0:MM}+=/{0:dd}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}+=/{0:HH}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}+=/{0:mm}",
                "<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}"
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var selectQuery = String.Format("/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic firstMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic secondMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstMinuteEntry);
            Assert.NotNull(secondMinuteEntry);
            Assert.Equal(((INode)firstMinuteEntry).Id, ((INode)secondMinuteEntry).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_4()
        {
            // Arrange.
            var now = DateTime.Now;
            var addQueries = new[]
            {
                "/Time+=/{0:yyyy}",
                "/Time/{0:yyyy}+=/{0:MM}",
                "/Time/{0:yyyy}/{0:MM}+=/{0:dd}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}+=/{0:HH}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}+=/{0:mm}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var selectQuery = String.Format("<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic firstMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic secondMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstMinuteEntry);
            Assert.NotNull(secondMinuteEntry);
            Assert.Equal(((INode)firstMinuteEntry).Id, ((INode)secondMinuteEntry).Id);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_At_Once_1()
        {
            // Arrange.
            var now = DateTime.Now;
            var addQueries = new[]
            {
                "<= /Time+=/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}"
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var selectQuery = String.Format("<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic firstMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic secondMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstMinuteEntry);
            Assert.NotNull(secondMinuteEntry);
            Assert.Equal(((INode)firstMinuteEntry).Id, ((INode)secondMinuteEntry).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_At_Once_2()
        {
            // Arrange.
            var now = DateTime.Now;
            var addQueries = new[]
            {
                "/Time+=/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
                "<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var selectQuery = String.Format("<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic firstMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic secondMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstMinuteEntry);
            Assert.NotNull(secondMinuteEntry);
            Assert.Equal(((INode)firstMinuteEntry).Id, ((INode)secondMinuteEntry).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_At_Once_3()
        {
            // Arrange.
            var now = DateTime.Now;
            var addQueries = new[]
            {
                "/Time+=/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var selectQuery = String.Format("<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic firstMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic secondMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstMinuteEntry);
            Assert.NotNull(secondMinuteEntry);
            Assert.Equal(((INode)firstMinuteEntry).Id, ((INode)secondMinuteEntry).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_At_Once_4()
        {
            // Arrange.
            var now = DateTime.Now;
            var addQueries = new[]
            {
                "/Time+=/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
                "<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var selectQuery = String.Format("/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic firstMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic secondMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstMinuteEntry);
            Assert.NotNull(secondMinuteEntry);
            Assert.Equal(((INode)firstMinuteEntry).Id, ((INode)secondMinuteEntry).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_Spaced_1()
        {
            // Arrange.
            var now = DateTime.Now;
            var addQueries = new[]
            {
                "/Time +=/{0:yyyy}",
                "/Time/{0:yyyy} +=/{0:MM}",
                "/Time/{0:yyyy}/{0:MM} +=/{0:dd}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd} +=/{0:HH}",
                "<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH} +=/{0:mm}",
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var selectQuery = String.Format("/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic firstMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic secondMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstMinuteEntry);
            Assert.NotNull(secondMinuteEntry);
            Assert.Equal(((INode)firstMinuteEntry).Id, ((INode)secondMinuteEntry).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_Spaced_2()
        {
            // Arrange.
            var now = DateTime.Now;
            var addQueries = new[]
            {
                "/Time+= /{0:yyyy}",
                "/Time/{0:yyyy}+= /{0:MM}",
                "/Time/{0:yyyy}/{0:MM}+= /{0:dd}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}+= /{0:HH}",
                "<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}+= /{0:mm}"
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var selectQuery = String.Format("/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic firstMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic secondMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstMinuteEntry);
            Assert.NotNull(secondMinuteEntry);
            Assert.Equal(((INode)firstMinuteEntry).Id, ((INode)secondMinuteEntry).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Advanced_Add_Tree_At_Once()
        {
            // Arrange.
            var addQuery = "<= /Person+=/Doe/John";
            var selectQuery = "/Person/Doe/John";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic firstJohnEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic secondJohnEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstJohnEntry);
            Assert.NotNull(secondJohnEntry);
            Assert.Equal(((INode)firstJohnEntry).Id, ((INode)secondJohnEntry).Id);

        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Advanced_Add_Tree_At_Once_From_Root_NonSpaced_01()
        {
            // Arrange.
            var addQuery = "+=/Person/Doe/John";
            var selectQuery = "/Person/Doe/John";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic firstJohnEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic secondJohnEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.Null(firstJohnEntry);//, "First entry is not null");
            Assert.NotNull(secondJohnEntry);//, "Second entry is null");
            Assert.Equal("John", secondJohnEntry.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Advanced_Add_Tree_At_Once_From_Root_Spaced_02()
        {
            // Arrange.
            var addQuery = "+= /Person/Doe/John";
            var selectQuery = "/Person/Doe/John";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic firstJohnEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic secondJohnEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.Null(firstJohnEntry);//, "First entry is not null");
            Assert.NotNull(secondJohnEntry);//, "Second entry is null");
            Assert.Equal("John", secondJohnEntry.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Advanced_Add_Tree_At_Once_From_Root_Wrong_Root()
        {
            // Arrange.
            var addQuery = "+=/Person_Bad/Doe/John";

            var addScript = _parser.Parse(addQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var act = processor.Process(addScript);

            // Assert.
            await ExceptionAssert.ThrowsObservable<ScriptProcessingException, SequenceProcessingResult>(act); 
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Advanced_Add_Tree_At_Once_Spaced()
        {
            // Arrange.
            var addQuery = "<= /Person += /Doe/John";
            var selectQuery = "/Person/Doe/John";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic firstJohnEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic secondJohnEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstJohnEntry);
            Assert.NotNull(secondJohnEntry);
            Assert.Equal(((INode)firstJohnEntry).Id, ((INode)secondJohnEntry).Id);
        }
    }
}