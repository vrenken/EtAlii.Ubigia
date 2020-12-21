namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using Xunit;

    public class DowndateOfPathSubjectPartTests : IDisposable
    {
        //private IScriptParser _parser

        //public DowndateOfPathSubjectPartTests()
        //[
            //var diagnostics = TestDiagnostics.Create()
            //var scriptParserConfiguration = new ScriptParserConfiguration()
            //    .Use(diagnostics)
            //_parser = new ScriptParserFactory().Create(scriptParserConfiguration)
        //]

        public void Dispose()
        {
            //_parser = null
            GC.SuppressFinalize(this);
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
