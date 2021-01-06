// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;
    using Xunit;

    public class SequenceParserTests : IDisposable
    {
        private ISequenceParser _parser;

        public SequenceParserTests()
        {
            var container = new Container();

            new LapaConstantParsingScaffolding().Register(container);
            new LapaScriptParserScaffolding().Register(container);
            new LapaSequenceParsingScaffolding().Register(container);
            new LapaOperatorParsingScaffolding().Register(container);
            new LapaSubjectParsingScaffolding().Register(container);
            new LapaPathSubjectParsingScaffolding().Register(container);

            _parser = container.GetInstance<ISequenceParser>();
        }

        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_Constants_01()
        {
            // Arrange.
            var text = "'First' <= 'Second' <= 'Third'";
            var scriptValidator = new ScriptValidator();

            // Act.
            var act = new Action(() =>
            {
                var sequence = _parser.Parse(text);
                scriptValidator.Validate(sequence);
            });

            // Assert.
            Assert.Throws<ScriptParserException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_Constants_02()
        {
            // Arrange.
            var text = @"""First"" <= ""Second"" <= ""Third""";
            var scriptValidator = new ScriptValidator();

            // Act.
            var act = new Action(() =>
            {
                var sequence = _parser.Parse(text);
                scriptValidator.Validate(sequence);
            });

            // Assert.
            Assert.Throws<ScriptParserException>(act);

        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_Constants_03()
        {
            // Arrange.
            var text = @"""First"" <= 'Second' <= ""Third""";
            var scriptValidator = new ScriptValidator();

            // Act.
            var act = new Action(() =>
            {
                var sequence = _parser.Parse(text);
                scriptValidator.Validate(sequence);
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
            var scriptValidator = new ScriptValidator();

            // Act.
            var act = new Action(() =>
            {
                var sequence = _parser.Parse(text);
                scriptValidator.Validate(sequence);
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
            var scriptValidator = new ScriptValidator();

            // Act.
            var act = new Action(() =>
            {
                var sequence = _parser.Parse(text);
                scriptValidator.Validate(sequence);
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
