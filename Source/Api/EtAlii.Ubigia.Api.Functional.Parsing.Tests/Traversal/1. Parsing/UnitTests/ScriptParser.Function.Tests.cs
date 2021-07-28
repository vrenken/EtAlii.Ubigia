// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Functional.Traversal.Tests;
    using Xunit;

    public class ScriptParserFunctionTests : IClassFixture<TraversalUnitTestContext>, IDisposable
    {
        private IScriptParser _parser;

        public ScriptParserFunctionTests(TraversalUnitTestContext testContext)
        {
            _parser = new TestScriptParserFactory().Create(testContext.ClientConfiguration);
        }

        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Function()
        {
            // Arrange.
            const string query = "function()";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Empty(part.Arguments);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Invalid_01()
        {
            // Arrange.
            const string text = "function()()";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Invalid_02()
        {
            // Arrange.
            const string text = "()function";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Invalid_03()
        {
            // Arrange.
            const string text = "(function)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_SingleQuoted()
        {
            // Arrange.
            const string query = "function('First')";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Single(part.Arguments);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_DoubleQuoted()
        {
            // Arrange.
            const string query = "function(\"First\")";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Single(part.Arguments);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_SingleQuoted_Special_Characters()
        {
            // Arrange.
            const string query = "function('First äëöüáéóúâêôû')";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Single(part.Arguments);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("First äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_DoubleQuoted_Special_Characters()
        {
            // Arrange.
            const string query = "function(\"First äëöüáéóúâêôû\")";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Single(part.Arguments);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("First äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }





        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_SingleQuoted_Invalid_01()
        {
            // Arrange.
            const string text = "function('First'')";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_SingleQuoted_Invalid_02()
        {
            // Arrange.
            const string text = "function(''First')";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_SingleQuoted_Invalid_03()
        {
            // Arrange.
            const string text = "function('Fi'rst')";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_DoubleQuoted_Invalid_01()
        {
            // Arrange.
            const string text = "function(\"First\"\")";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_DoubleQuoted_Invalid_02()
        {
            // Arrange.
            const string text = "function(\"\"First\")";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_DoubleQuoted_Invalid_03()
        {
            // Arrange.
            const string text = "function(\"Fi\"rst\")";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }




        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_DoubleQuoted_Invalid_Special_Characters_01()
        {
            // Arrange.
            const string text = "function(\"First\" äëöüáéóúâêôû\")";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_DoubleQuoted_Invalid_Special_Characters_02()
        {
            // Arrange.
            const string text = "function(\"\"First äëöüáéóúâêôû\")";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_DoubleQuoted_Invalid_Special_Characters_03()
        {
            // Arrange.
            const string text = "function(\"Fi\"rst äëöüáéóúâêôû\")";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }



        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Multiple_SingleQuoted()
        {
            // Arrange.
            const string query = "function('First','Second', 'Third')";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Equal(3, part.Arguments.Length);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[1]);
            Assert.Equal("Second", ((ConstantFunctionSubjectArgument)part.Arguments[1]).Value);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[2]);
            Assert.Equal("Third", ((ConstantFunctionSubjectArgument)part.Arguments[2]).Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Multiple_DoubleQuoted()
        {
            // Arrange.
            const string query = "function(\"First\",\"Second\", \"Third\")";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Equal(3, part.Arguments.Length);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[1]);
            Assert.Equal("Second", ((ConstantFunctionSubjectArgument)part.Arguments[1]).Value);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[2]);
            Assert.Equal("Third", ((ConstantFunctionSubjectArgument)part.Arguments[2]).Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Multiple_SingleQuoted_Special_Characters()
        {
            // Arrange.
            const string query = "function('First äëöüáéóúâêôû','Second äëöüáéóúâêôû', 'Third äëöüáéóúâêôû')";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Equal(3, part.Arguments.Length);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("First äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[1]);
            Assert.Equal("Second äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[1]).Value);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[2]);
            Assert.Equal("Third äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[2]).Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Multiple_DoubleQuoted_Special_Characters()
        {
            // Arrange.
            const string query = "function(\"First äëöüáéóúâêôû\",\"Second äëöüáéóúâêôû\", \"Third äëöüáéóúâêôû\")";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Equal(3, part.Arguments.Length);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("First äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[1]);
            Assert.Equal("Second äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[1]).Value);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[2]);
            Assert.Equal("Third äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[2]).Value);
        }











        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Assignment()
        {
            // Arrange.
            const string query = "/Document/Images <= function()";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(2).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Empty(part.Arguments);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Assignment_Constant_Parameter_Single_SingleQuoted()
        {
            // Arrange.
            const string query = "/Document/Images <= function('First')";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(2).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Single(part.Arguments);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Assignment_Constant_Parameter_Single_DoubleQuoted()
        {
            // Arrange.
            const string query = "/Document/Images <= function(\"First\")";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(2).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Single(part.Arguments);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Assignment_Constant_Parameter_Multiple_SingleQuoted()
        {
            // Arrange.
            const string query = "/Document/Images <= function('First','Second', 'Third')";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(2).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Equal(3, part.Arguments.Length);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[1]);
            Assert.Equal("Second", ((ConstantFunctionSubjectArgument)part.Arguments[1]).Value);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[2]);
            Assert.Equal("Third", ((ConstantFunctionSubjectArgument)part.Arguments[2]).Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Assignment_Constant_Parameter_Multiple_SingleQuoted_Special_Characters()
        {
            // Arrange.
            const string query = "/Document/Images <= function('First äëöüáéóúâêôû','Second äëöüáéóúâêôû', 'Third äëöüáéóúâêôû')";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(2).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Equal(3, part.Arguments.Length);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("First äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[1]);
            Assert.Equal("Second äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[1]).Value);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[2]);
            Assert.Equal("Third äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[2]).Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Assignment_Constant_Parameter_Multiple_DoubleQuoted()
        {
            // Arrange.
            const string query = "/Document/Images <= function(\"First\",\"Second\", \"Third\")";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(2).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Equal(3, part.Arguments.Length);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[1]);
            Assert.Equal("Second", ((ConstantFunctionSubjectArgument)part.Arguments[1]).Value);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[2]);
            Assert.Equal("Third", ((ConstantFunctionSubjectArgument)part.Arguments[2]).Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Assignment_Constant_Parameter_Multiple_DoubleQuoted_Special_Characters()
        {
            // Arrange.
            const string query = "/Document/Images <= function(\"First äëöüáéóúâêôû\",\"Second äëöüáéóúâêôû\", \"Third äëöüáéóúâêôû\")";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(2).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Equal(3, part.Arguments.Length);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("First äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[1]);
            Assert.Equal("Second äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[1]).Value);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[2]);
            Assert.Equal("Third äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[2]).Value);
        }



















        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Process()
        {
            // Arrange.
            const string query = "function() <= /Document/Images";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Empty(part.Arguments);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Process_Constant_Parameter_Single_SingleQuoted()
        {
            // Arrange.
            const string query = "function('First') <= /Document/Images";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Single(part.Arguments);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Process_Constant_Parameter_Single_DoubleQuoted()
        {
            // Arrange.
            const string query = "function(\"First\") <= /Document/Images";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Single(part.Arguments);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Process_Constant_Parameter_Multiple_SingleQuoted()
        {
            // Arrange.
            const string query = "function('First','Second', 'Third') <= /Document/Images";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Equal(3, part.Arguments.Length);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[1]);
            Assert.Equal("Second", ((ConstantFunctionSubjectArgument)part.Arguments[1]).Value);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[2]);
            Assert.Equal("Third", ((ConstantFunctionSubjectArgument)part.Arguments[2]).Value);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Process_Constant_Parameter_Multiple_DoubleQuoted()
        {
            // Arrange.
            const string query = "function(\"First\",\"Second\", \"Third\") <= /Document/Images";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Equal(3, part.Arguments.Length);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[1]);
            Assert.Equal("Second", ((ConstantFunctionSubjectArgument)part.Arguments[1]).Value);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[2]);
            Assert.Equal("Third", ((ConstantFunctionSubjectArgument)part.Arguments[2]).Value);
        }






        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Single()
        {
            // Arrange.
            const string query = "function($var1)";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Single(part.Arguments);
            Assert.IsType<VariableFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("var1", ((VariableFunctionSubjectArgument)part.Arguments[0]).Name);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Single_Invalid_01()
        {
            // Arrange.
            const string text = "function($var1$var2)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Single_Invalid_02()
        {
            // Arrange.
            const string text = "function($$var1)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Single_Invalid_03()
        {
            // Arrange.
            const string text = "function(\"\"$var1)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Single_Invalid_04()
        {
            // Arrange.
            const string text = "function(''$var1)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple()
        {
            // Arrange.
            const string query = "function($var1, $var2)";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("function", part.Name);
            Assert.Equal(2, part.Arguments.Length);
            Assert.IsType<VariableFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("var1", ((VariableFunctionSubjectArgument)part.Arguments[0]).Name);
            Assert.IsType<VariableFunctionSubjectArgument>(part.Arguments[1]);
            Assert.Equal("var2", ((VariableFunctionSubjectArgument)part.Arguments[1]).Name);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_01()
        {
            // Arrange.
            const string text = "function($var0, $var1$var2)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_02()
        {
            // Arrange.
            const string text = "function($var0, $$var1)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_03()
        {
            // Arrange.
            const string text = "function($var0, \"\"$var1)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_04()
        {
            // Arrange.
            const string text = "function($var0, ''$var1)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_05()
        {
            // Arrange.
            const string text = "function($var1$var2, var0)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_06()
        {
            // Arrange.
            const string text = "function($$var1, var0)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_07()
        {
            // Arrange.
            const string text = "function(\"\"$var1, var0)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_08()
        {
            // Arrange.
            const string text = "function(''$var1, var0)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_09()
        {
            // Arrange.
            const string text = "function($var1$, var0)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_10()
        {
            // Arrange.
            const string text = "function($var1\"\", var0)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_11()
        {
            // Arrange.
            const string text = "function($var1'', var0)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_12()
        {
            // Arrange.
            const string text = "function($var0, $var1$)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_13()
        {
            // Arrange.
            const string text = "function($var0, $var1\"\")";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_14()
        {
            // Arrange.
            const string text = "function($var0, $var1'')";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_15()
        {
            // Arrange.
            const string text = "function($var0, $var1,)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_16()
        {
            // Arrange.
            const string text = "function(,$var0, $var1)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_17()
        {
            // Arrange.
            const string text = "function(,$var0)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_18()
        {
            // Arrange.
            const string text = "function($var0,)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_19()
        {
            // Arrange.
            const string text = "function($ var0)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }
    }
}
