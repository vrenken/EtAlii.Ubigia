namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using Xunit;

    public class ConstantPathSubjectPartTests //: IDisposable
    {
        //private IScriptParser _parser

        //public ConstantPathSubjectPartTests()
        //[
            //var diagnostics = TestDiagnostics.Create()
            //var scriptParserConfiguration = new ScriptParserConfiguration()
            //    .UseFunctionalDiagnostics(diagnostics)
            //_parser = new ScriptParserFactory().Create(scriptParserConfiguration)
        //]

        //public void Dispose()
        //[
            //_parser = null
        //]

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ConstantPathSubjectPart_ToString()
        {
            // Arrange.
            var part = new ConstantPathSubjectPart("Test");

            // Act.
            var result = part.ToString();

            // Assert.
            Assert.Equal("Test", result);
        }
    }
}
