// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Functional.Traversal.Tests;
    using EtAlii.xTechnology.MicroContainer;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class SequenceParserTests : IClassFixture<TraversalUnitTestContext>, IAsyncLifetime
    {
        private ISequenceParser _parser;
        private readonly TraversalUnitTestContext _testContext;

        public SequenceParserTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            var container = new Container();

            var options = await new FunctionalOptions(_testContext.ClientConfiguration)
                .UseTestParsing()
                .UseDataConnectionToNewSpace(_testContext, true)
                .ConfigureAwait(false);
            new LapaParserExtension(options).Initialize(container);

            _parser = container.GetInstance<ISequenceParser>();
        }

        public Task DisposeAsync()
        {
            _parser = null;
            return Task.CompletedTask;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_Constants_01()
        {
            // Arrange.
            var text = "'First' <= 'Second' <= 'Third'";
            var validator = new TraversalValidator();

            // Act.
            var act = new Action(() =>
            {
                var sequence = _parser.Parse(text);
                validator.Validate(sequence);
            });

            // Assert.
            Assert.Throws<ScriptParserException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_Constants_02()
        {
            // Arrange.
            var text = @"""First"" <= ""Second"" <= ""Third""";
            var validator = new TraversalValidator();

            // Act.
            var act = new Action(() =>
            {
                var sequence = _parser.Parse(text);
                validator.Validate(sequence);
            });

            // Assert.
            Assert.Throws<ScriptParserException>(act);

        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_Constants_03()
        {
            // Arrange.
            var text = @"""First"" <= 'Second' <= ""Third""";
            var validator = new TraversalValidator();

            // Act.
            var act = new Action(() =>
            {
                var sequence = _parser.Parse(text);
                validator.Validate(sequence);
            });

            // Assert.
            Assert.Throws<ScriptParserException>(act);

        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_Variable_01()
        {
            // Arrange.
            var text = @"$first <= ""Second""";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 3);
            Assert.IsType<VariableSubject>(sequence.Parts.ElementAt(0));
            Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));
            Assert.IsType<StringConstantSubject>(sequence.Parts.ElementAt(2));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_Variable_02()
        {
            // Arrange.
            var text = @"/'Second' <= $first <= ""Third""";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 5);
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.ElementAt(0));
            Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));
            Assert.IsType<VariableSubject>(sequence.Parts.ElementAt(2));
            Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(3));
            Assert.IsType<StringConstantSubject>(sequence.Parts.ElementAt(4));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_Variable_03_Invalid()
        {
            // Arrange.
            var text = @"""Second"" <= $first";
            var validator = new TraversalValidator();

            // Act.
            var act = new Action(() =>
            {
                var sequence = _parser.Parse(text);
                validator.Validate(sequence);
            });

            // Assert.
            Assert.Throws<ScriptParserException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_Variable_03()
        {
            // Arrange.
            var text = @"/""Second"" <= $first";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 3);
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.ElementAt(0));
            Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));
            Assert.IsType<VariableSubject>(sequence.Parts.ElementAt(2));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_Variable_04_Invalid()
        {
            // Arrange.
            var text = @"'Second' <= $first <= ""Third""";
            var validator = new TraversalValidator();

            // Act.
            var act = new Action(() =>
            {
                var sequence = _parser.Parse(text);
                validator.Validate(sequence);
            });

            // Assert.
            Assert.Throws<ScriptParserException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_Variable_04()
        {
            // Arrange.
            var text = @"/'Second' <= $first <= ""Third""";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 5);
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.ElementAt(0));
            Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));
            Assert.IsType<VariableSubject>(sequence.Parts.ElementAt(2));
            Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(3));
            Assert.IsType<StringConstantSubject>(sequence.Parts.ElementAt(4));
        }
    }
}
