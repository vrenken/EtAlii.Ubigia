namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptParserFunctionNewTests : IDisposable
    {
        private IScriptParser _parser;

        public ScriptParserFunctionNewTests()
        {
            var diagnostics = DiagnosticsConfiguration.Default;
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .UseFunctionalDiagnostics(diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }

        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_New_Blank()
        {
            // Arrange.
            var scriptText = new[]
            {
                "Person:Doe/John/Visits += new()",
            };

            // Act.
            var result = _parser.Parse(scriptText);

            // Assert.
            Assert.NotNull(result);
            Assert.Empty(result.Errors);
            var script = result.Script;
            Assert.NotNull(script);
            var sequence = script.Sequences.FirstOrDefault();
            Assert.NotNull(sequence);
            Assert.Equal(3, sequence.Parts.Length);
            Assert.IsType<RootedPathSubject>(sequence.Parts[0]);
            Assert.IsType<AddOperator>(sequence.Parts[1]);
            Assert.IsType<FunctionSubject>(sequence.Parts[2]);
            var functionSubject = (FunctionSubject)sequence.Parts[2];
            Assert.Equal("new", functionSubject.Name);
            Assert.Empty(functionSubject.Arguments);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_New_Argument_01()
        {
            // Arrange.
            var scriptText = new[]
            {
                "Person:Doe/John/Visits += new('Vacation')",
            };

            // Act.
            var result = _parser.Parse(scriptText);

            // Assert.
            Assert.NotNull(result);
            Assert.Empty(result.Errors);
            var script = result.Script;
            Assert.NotNull(script);
            var sequence = script.Sequences.FirstOrDefault();
            Assert.NotNull(sequence);
            Assert.Equal(3, sequence.Parts.Length);
            Assert.IsType<RootedPathSubject>(sequence.Parts[0]);
            Assert.IsType<AddOperator>(sequence.Parts[1]);
            Assert.IsType<FunctionSubject>(sequence.Parts[2]);
            var functionSubject = (FunctionSubject)sequence.Parts[2];
            Assert.Equal("new", functionSubject.Name);
            Assert.NotEmpty(functionSubject.Arguments);
            Assert.IsType<ConstantFunctionSubjectArgument>(functionSubject.Arguments[0]);
            var argument = (ConstantFunctionSubjectArgument)functionSubject.Arguments[0];
            Assert.Equal("Vacation", argument.Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_New_Argument_02()
        {
            // Arrange.
            var scriptText = new[]
            {
                "Person:Doe/John/Visits += new(\"Vacation\")",
            };

            // Act.
            var result = _parser.Parse(scriptText);

            // Assert.
            Assert.NotNull(result);
            Assert.Empty(result.Errors);
            var script = result.Script;
            Assert.NotNull(script);
            var sequence = script.Sequences.FirstOrDefault();
            Assert.NotNull(sequence);
            Assert.Equal(3, sequence.Parts.Length);
            Assert.IsType<RootedPathSubject>(sequence.Parts[0]);
            Assert.IsType<AddOperator>(sequence.Parts[1]);
            Assert.IsType<FunctionSubject>(sequence.Parts[2]);
            var functionSubject = (FunctionSubject)sequence.Parts[2];
            Assert.Equal("new", functionSubject.Name);
            Assert.NotEmpty(functionSubject.Arguments);
            Assert.IsType<ConstantFunctionSubjectArgument>(functionSubject.Arguments[0]);
            var argument = (ConstantFunctionSubjectArgument)functionSubject.Arguments[0];
            Assert.Equal("Vacation", argument.Value);
        }
    }
}
