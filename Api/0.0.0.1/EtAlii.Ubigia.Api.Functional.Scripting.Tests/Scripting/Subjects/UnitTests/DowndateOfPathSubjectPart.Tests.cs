namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting;
    using Xunit;


    public class DowndateOfPathSubjectPartTests : IAsyncLifetime
    {
        private IScriptParser _parser;

        public Task InitializeAsync()
        {
            var diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .Use(diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            _parser = null;
            return Task.CompletedTask;
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void PreviousSubjectPart_ToString()
        {
            // Arrange.
            var part = new DowndatePathSubjectPart();

            // Act.
            var result = part.ToString();

            // Assert.
            Assert.Equal("{", result);
        }
    }
}