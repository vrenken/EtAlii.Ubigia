// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;
    using Xunit;

    public class SequenceParserPathsTemporalTests : IDisposable
    {
        private ISequenceParser _parser;

        public SequenceParserPathsTemporalTests()
        {
            var container = new Container();

            new ConstantHelpersScaffolding().Register(container);
            new LapaScriptParserScaffolding().Register(container);
            new SequenceParsingScaffolding().Register(container);
            new OperatorParsingScaffolding().Register(container);
            new SubjectParsingScaffolding().Register(container);
            new PathSubjectParsingScaffolding().Register(container);

            _parser = container.GetInstance<ISequenceParser>();
        }

        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Hierarchical_Downdate_01()
        {
            // Arrange.
            var text = "/First/Second/Third{";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(7, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<DowndatePathSubjectPart>(pathSubject.Parts.ElementAt(6));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Hierarchical_Downdate_02()
        {
            // Arrange.
            var text = "/First/Second/Third/{";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(8, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(6));
            Assert.IsType<DowndatePathSubjectPart>(pathSubject.Parts.ElementAt(7));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Hierarchical_Downdate_03()
        {
            // Arrange.
            var text = "/First/Second{/Third/";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(8, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<DowndatePathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(6));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(7));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Hierarchical_Downdate_04()
        {
            // Arrange.
            var text = "/First/{Second/Third/";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(8, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<DowndatePathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(6));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(7));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Hierarchical_Downdate_05()
        {
            // Arrange.
            var text = "/First/{{Second/Third/";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(8, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<AllDowndatesPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(6));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(7));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Hierarchical_Downdate_06()
        {
            // Arrange.
            var text = "/First/Second/Third{{";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(7, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<AllDowndatesPathSubjectPart>(pathSubject.Parts.ElementAt(6));
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Hierarchical_Downdate_07()
        {
            // Arrange.
            var text = "/First/Second/Third{{{";
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
        public void SequenceParser_Parse_PathSubject_Hierarchical_Downdate_08()
        {
            // Arrange.
            var text = "/First/{{{Second/Third";
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
        public void SequenceParser_Parse_PathSubject_Hierarchical_Update_01()
        {
            // Arrange.
            var text = "/First/Second/Third}";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(7, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<UpdatesPathSubjectPart>(pathSubject.Parts.ElementAt(6));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Hierarchical_Update_02()
        {
            // Arrange.
            var text = "/First/Second/Third/}";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(8, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(6));
            Assert.IsType<UpdatesPathSubjectPart>(pathSubject.Parts.ElementAt(7));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Hierarchical_Update_03()
        {
            // Arrange.
            var text = "/First/Second}/Third/";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(8, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<UpdatesPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(6));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(7));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Hierarchical_Update_04()
        {
            // Arrange.
            var text = "/First/}Second/Third/";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(8, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<UpdatesPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(6));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(7));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Hierarchical_Update_05()
        {
            // Arrange.
            var text = "/First/}}Second/Third/";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(8, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<AllUpdatesPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(6));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(7));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Hierarchical_Update_07()
        {
            // Arrange.
            var text = "/First/Second/Third}}";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(7, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<AllUpdatesPathSubjectPart>(pathSubject.Parts.ElementAt(6));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Hierarchical_Update_08()
        {
            // Arrange.
            var text = "/First/Second/Third}}}";
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
        public void SequenceParser_Parse_PathSubject_Hierarchical_Update_09()
        {
            // Arrange.
            var text = "/First/}}}Second/Third";
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

    }
}
