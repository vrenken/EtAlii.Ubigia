// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.xTechnology.MicroContainer;
    using Xunit;


    
    public class SequenceParserPathsTemporalTests : IDisposable
    {
        private ISequenceParser _parser;

        public SequenceParserPathsTemporalTests()
        {
            var container = new Container();

            new ConstantHelpersScaffolding().Register(container); 
            new ScriptParserScaffolding().Register(container);
            new SequenceParsingScaffolding().Register(container);
            new OperatorParsingScaffolding().Register(container);
            new SubjectParsingScaffolding().Register(container);

            _parser = container.GetInstance<ISequenceParser>();
        }

        public void Dispose()
        {
            _parser = null;
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
            Assert.True(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(7, pathSubject.Parts.Count());
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<DowndateOfPathSubjectPart>(pathSubject.Parts.ElementAt(6));
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
            Assert.True(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(8, pathSubject.Parts.Count());
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(6));
            Assert.IsType<DowndateOfPathSubjectPart>(pathSubject.Parts.ElementAt(7));
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
            Assert.True(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(8, pathSubject.Parts.Count());
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<DowndateOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(6));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(7));
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
            Assert.True(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(8, pathSubject.Parts.Count());
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<DowndateOfPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(6));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(7));
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
            Assert.True(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(9, pathSubject.Parts.Count());
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<DowndateOfPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<DowndateOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(6));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(7));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(8));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Hierarchical_Downdate_06()
        {
            // Arrange.
            var text = "/First/Second/Third{{{";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(9, pathSubject.Parts.Count());
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<DowndateOfPathSubjectPart>(pathSubject.Parts.ElementAt(6));
            Assert.IsType<DowndateOfPathSubjectPart>(pathSubject.Parts.ElementAt(7));
            Assert.IsType<DowndateOfPathSubjectPart>(pathSubject.Parts.ElementAt(8));
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
            Assert.True(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(7, pathSubject.Parts.Count());
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<UpdatesOfPathSubjectPart>(pathSubject.Parts.ElementAt(6));
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
            Assert.True(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(8, pathSubject.Parts.Count());
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(6));
            Assert.IsType<UpdatesOfPathSubjectPart>(pathSubject.Parts.ElementAt(7));
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
            Assert.True(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(8, pathSubject.Parts.Count());
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<UpdatesOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(6));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(7));
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
            Assert.True(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(8, pathSubject.Parts.Count());
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<UpdatesOfPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(6));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(7));
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
            Assert.True(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(9, pathSubject.Parts.Count());
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<UpdatesOfPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<UpdatesOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(6));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(7));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(8));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Hierarchical_Update_06()
        {
            // Arrange.
            var text = "/First/Second/Third}}}";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(9, pathSubject.Parts.Count());
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.IsType<UpdatesOfPathSubjectPart>(pathSubject.Parts.ElementAt(6));
            Assert.IsType<UpdatesOfPathSubjectPart>(pathSubject.Parts.ElementAt(7));
            Assert.IsType<UpdatesOfPathSubjectPart>(pathSubject.Parts.ElementAt(8));
        }
    }
}