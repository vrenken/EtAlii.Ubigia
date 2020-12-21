﻿// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;
    using Xunit;

    public class SequenceParserPathsHierarchicalTests : IDisposable
    {
        private ISequenceParser _parser;

        public SequenceParserPathsHierarchicalTests()
        {
            var container = new Container();

            new ConstantHelpersScaffolding().Register(container);
            new ScriptParserScaffolding().Register(container);
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
        public void SequenceParser_Parse_PathSubject_Absolute_01()
        {
            // Arrange.
            var text = "/First";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(2, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_03()
        {
            // Arrange.
            var text = "/First/Second/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_04()
        {
            // Arrange.
            var text = "$var <= /First/Second/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 3);

            var variableSubject = sequence.Parts.ElementAt(0) as VariableSubject;
            Assert.NotNull(variableSubject);
            Assert.Equal("var", variableSubject.Name);

            Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));

            var pathSubject = sequence.Parts.ElementAt(2) as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.Equal("First", ((ConstantPathSubjectPart) pathSubject.Parts.ElementAt(1)).Name);
            Assert.Equal("Second", ((ConstantPathSubjectPart) pathSubject.Parts.ElementAt(3)).Name);
            Assert.Equal("Third", ((ConstantPathSubjectPart) pathSubject.Parts.ElementAt(5)).Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_01()
        {
            // Arrange.
            var text = "/\"First\"";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(2, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_02()
        {
            // Arrange.
            var text = "/\"First\"/\"Second\"/\"Third\"/";

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
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(6));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_03()
        {
            // Arrange.
            var text = "/\"First\"/\"Second\"/\"Third\"";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_04()
        {
            // Arrange.
            var text = "$var <= /\"First\"/\"Second\"/\"Third\"";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 3);

            var variableSubject = sequence.Parts.ElementAt(0) as VariableSubject;
            Assert.NotNull(variableSubject);
            Assert.Equal("var", variableSubject.Name);

            Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));

            var pathSubject = sequence.Parts.ElementAt(2) as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.Equal("First", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(1)).Name);
            Assert.Equal("Second", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(3)).Name);
            Assert.Equal("Third", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(5)).Name);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_05()
        {
            // Arrange.
            var text = "$var <= /First/\"Second\"/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 3);

            var variableSubject = sequence.Parts.ElementAt(0) as VariableSubject;
            Assert.NotNull(variableSubject);
            Assert.Equal("var", variableSubject.Name);

            Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));

            var pathSubject = sequence.Parts.ElementAt(2) as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.Equal("First", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(1)).Name);
            Assert.Equal("Second", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(3)).Name);
            Assert.Equal("Third", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(5)).Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Relative_Quoted_01()
        {
            // Arrange.
            var text = "/Person += First/\"Second\"/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 3);

            var pathSubject = sequence.Parts.ElementAt(0) as AbsolutePathSubject;
            Assert.NotNull(pathSubject);

            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));

            var relativePathSubject = sequence.Parts.ElementAt(2) as RelativePathSubject;
            Assert.NotNull(relativePathSubject);
            Assert.Equal(5, relativePathSubject.Parts.Length);
            Assert.IsType<ConstantPathSubjectPart>(relativePathSubject.Parts.ElementAt(0));
            Assert.IsType<ParentPathSubjectPart>(relativePathSubject.Parts.ElementAt(1));
            Assert.IsType<ConstantPathSubjectPart>(relativePathSubject.Parts.ElementAt(2));
            Assert.IsType<ParentPathSubjectPart>(relativePathSubject.Parts.ElementAt(3));
            Assert.IsType<ConstantPathSubjectPart>(relativePathSubject.Parts.ElementAt(4));
            Assert.Equal("First", ((ConstantPathSubjectPart)relativePathSubject.Parts.ElementAt(0)).Name);
            Assert.Equal("Second", ((ConstantPathSubjectPart)relativePathSubject.Parts.ElementAt(2)).Name);
            Assert.Equal("Third", ((ConstantPathSubjectPart)relativePathSubject.Parts.ElementAt(4)).Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Relative_Quoted_02()
        {
            // Arrange.
            var text = "/Person += First/'Second'/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 3);

            var pathSubject = sequence.Parts.ElementAt(0) as AbsolutePathSubject;
            Assert.NotNull(pathSubject);

            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));

            var relativePathSubject = sequence.Parts.ElementAt(2) as RelativePathSubject;
            Assert.NotNull(relativePathSubject);
            Assert.Equal(5, relativePathSubject.Parts.Length);
            Assert.IsType<ConstantPathSubjectPart>(relativePathSubject.Parts.ElementAt(0));
            Assert.IsType<ParentPathSubjectPart>(relativePathSubject.Parts.ElementAt(1));
            Assert.IsType<ConstantPathSubjectPart>(relativePathSubject.Parts.ElementAt(2));
            Assert.IsType<ParentPathSubjectPart>(relativePathSubject.Parts.ElementAt(3));
            Assert.IsType<ConstantPathSubjectPart>(relativePathSubject.Parts.ElementAt(4));
            Assert.Equal("First", ((ConstantPathSubjectPart)relativePathSubject.Parts.ElementAt(0)).Name);
            Assert.Equal("Second", ((ConstantPathSubjectPart)relativePathSubject.Parts.ElementAt(2)).Name);
            Assert.Equal("Third", ((ConstantPathSubjectPart)relativePathSubject.Parts.ElementAt(4)).Name);
        }




        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Relative_Quoted_03()
        {
            // Arrange.
            var text = "/Person += \"First\"/\"Second\"/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 3);

            var pathSubject = sequence.Parts.ElementAt(0) as AbsolutePathSubject;
            Assert.NotNull(pathSubject);

            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));

            var relativePathSubject = sequence.Parts.ElementAt(2) as RelativePathSubject;
            Assert.NotNull(relativePathSubject);
            Assert.Equal(5, relativePathSubject.Parts.Length);
            Assert.IsType<ConstantPathSubjectPart>(relativePathSubject.Parts.ElementAt(0));
            Assert.IsType<ParentPathSubjectPart>(relativePathSubject.Parts.ElementAt(1));
            Assert.IsType<ConstantPathSubjectPart>(relativePathSubject.Parts.ElementAt(2));
            Assert.IsType<ParentPathSubjectPart>(relativePathSubject.Parts.ElementAt(3));
            Assert.IsType<ConstantPathSubjectPart>(relativePathSubject.Parts.ElementAt(4));
            Assert.Equal("First", ((ConstantPathSubjectPart)relativePathSubject.Parts.ElementAt(0)).Name);
            Assert.Equal("Second", ((ConstantPathSubjectPart)relativePathSubject.Parts.ElementAt(2)).Name);
            Assert.Equal("Third", ((ConstantPathSubjectPart)relativePathSubject.Parts.ElementAt(4)).Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Relative_Quoted_04()
        {
            // Arrange.
            var text = "/Person += 'First'/'Second'/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 3);

            var pathSubject = sequence.Parts.ElementAt(0) as AbsolutePathSubject;
            Assert.NotNull(pathSubject);

            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));

            var relativePathSubject = sequence.Parts.ElementAt(2) as RelativePathSubject;
            Assert.NotNull(relativePathSubject);
            Assert.Equal(5, relativePathSubject.Parts.Length);
            Assert.IsType<ConstantPathSubjectPart>(relativePathSubject.Parts.ElementAt(0));
            Assert.IsType<ParentPathSubjectPart>(relativePathSubject.Parts.ElementAt(1));
            Assert.IsType<ConstantPathSubjectPart>(relativePathSubject.Parts.ElementAt(2));
            Assert.IsType<ParentPathSubjectPart>(relativePathSubject.Parts.ElementAt(3));
            Assert.IsType<ConstantPathSubjectPart>(relativePathSubject.Parts.ElementAt(4));
            Assert.Equal("First", ((ConstantPathSubjectPart)relativePathSubject.Parts.ElementAt(0)).Name);
            Assert.Equal("Second", ((ConstantPathSubjectPart)relativePathSubject.Parts.ElementAt(2)).Name);
            Assert.Equal("Third", ((ConstantPathSubjectPart)relativePathSubject.Parts.ElementAt(4)).Name);
        }



        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Relative_Quoted_05()
        {
            // Arrange.
            var text = "/Person += \"First\"/Second/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 3);

            var pathSubject = sequence.Parts.ElementAt(0) as AbsolutePathSubject;
            Assert.NotNull(pathSubject);

            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));

            var relativePathSubject = sequence.Parts.ElementAt(2) as RelativePathSubject;
            Assert.NotNull(relativePathSubject);
            Assert.Equal(5, relativePathSubject.Parts.Length);
            Assert.IsType<ConstantPathSubjectPart>(relativePathSubject.Parts.ElementAt(0));
            Assert.IsType<ParentPathSubjectPart>(relativePathSubject.Parts.ElementAt(1));
            Assert.IsType<ConstantPathSubjectPart>(relativePathSubject.Parts.ElementAt(2));
            Assert.IsType<ParentPathSubjectPart>(relativePathSubject.Parts.ElementAt(3));
            Assert.IsType<ConstantPathSubjectPart>(relativePathSubject.Parts.ElementAt(4));
            Assert.Equal("First", ((ConstantPathSubjectPart)relativePathSubject.Parts.ElementAt(0)).Name);
            Assert.Equal("Second", ((ConstantPathSubjectPart)relativePathSubject.Parts.ElementAt(2)).Name);
            Assert.Equal("Third", ((ConstantPathSubjectPart)relativePathSubject.Parts.ElementAt(4)).Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Relative_Quoted_06()
        {
            // Arrange.
            var text = "/Person += 'First'/Second/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 3);

            var pathSubject = sequence.Parts.ElementAt(0) as AbsolutePathSubject;
            Assert.NotNull(pathSubject);

            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));

            var relativePathSubject = sequence.Parts.ElementAt(2) as RelativePathSubject;
            Assert.NotNull(relativePathSubject);
            Assert.Equal(5, relativePathSubject.Parts.Length);
            Assert.IsType<ConstantPathSubjectPart>(relativePathSubject.Parts.ElementAt(0));
            Assert.IsType<ParentPathSubjectPart>(relativePathSubject.Parts.ElementAt(1));
            Assert.IsType<ConstantPathSubjectPart>(relativePathSubject.Parts.ElementAt(2));
            Assert.IsType<ParentPathSubjectPart>(relativePathSubject.Parts.ElementAt(3));
            Assert.IsType<ConstantPathSubjectPart>(relativePathSubject.Parts.ElementAt(4));
            Assert.Equal("First", ((ConstantPathSubjectPart)relativePathSubject.Parts.ElementAt(0)).Name);
            Assert.Equal("Second", ((ConstantPathSubjectPart)relativePathSubject.Parts.ElementAt(2)).Name);
            Assert.Equal("Third", ((ConstantPathSubjectPart)relativePathSubject.Parts.ElementAt(4)).Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Relative_Quoted_07_Blank_SingleQuotes()
        {
            // Arrange.
            var text = "/Person += ''";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 3);

            var pathSubject = sequence.Parts.ElementAt(0) as AbsolutePathSubject;
            Assert.NotNull(pathSubject);

            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));

            var stringConstantSubject = sequence.Parts.ElementAt(2) as StringConstantSubject;
            Assert.NotNull(stringConstantSubject);
            Assert.Equal("", stringConstantSubject.Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Relative_Quoted_07_Blank_DoubleQuotes()
        {
            // Arrange.
            var text = "/Person += \"\"";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 3);

            var pathSubject = sequence.Parts.ElementAt(0) as AbsolutePathSubject;
            Assert.NotNull(pathSubject);

            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));

            var stringConstantSubject = sequence.Parts.ElementAt(2) as StringConstantSubject;
            Assert.NotNull(stringConstantSubject);
            Assert.Equal("", stringConstantSubject.Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_With_Point_01()
        {
            // Arrange.
            var text = "/\"F.irst\"";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(2, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_With_Point_02()
        {
            // Arrange.
            var text = "/\".First\"/\"S.econd\"/\"Third.\"/";

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
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(6));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_With_Point_03()
        {
            // Arrange.
            var text = "/\".First\"/\"S.econd\"/\"Third.\"";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_With_Point_04()
        {
            // Arrange.
            var text = "$var <= /\".First\"/\"S.econd\"/\"Third.\"";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 3);

            var variableSubject = sequence.Parts.ElementAt(0) as VariableSubject;
            Assert.NotNull(variableSubject);
            Assert.Equal("var", variableSubject.Name);

            Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));

            var pathSubject = sequence.Parts.ElementAt(2) as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.Equal(".First", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(1)).Name);
            Assert.Equal("S.econd", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(3)).Name);
            Assert.Equal("Third.", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(5)).Name);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_With_Point_05()
        {
            // Arrange.
            var text = "$var <= /First/\"S.econd\"/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 3);

            var variableSubject = sequence.Parts.ElementAt(0) as VariableSubject;
            Assert.NotNull(variableSubject);
            Assert.Equal("var", variableSubject.Name);

            Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));

            var pathSubject = sequence.Parts.ElementAt(2) as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.Equal("First", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(1)).Name);
            Assert.Equal("S.econd", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(3)).Name);
            Assert.Equal("Third", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(5)).Name);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_With_Empty_Path_01()
        {
            // Arrange.
            var text = "/\"\"";

            // Act.
            var act = new Action(() =>
            {
                _parser.Parse(text);
            });

            // Assert.
            Assert.Throws<ScriptParserException>(act);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_With_Empty_Path_02()
        {
            // Arrange.
            var text = "/\"\"/\"Second\"/\"\"/";

            // Act.
            var act = new Action(() =>
            {
                _parser.Parse(text);
            });

            // Assert.
            Assert.Throws<ScriptParserException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_With_Empty_Path_03()
        {
            // Arrange.
            var text = "/\"First\"/\"\"/\"\"";

            // Act.
            var act = new Action(() =>
            {
                _parser.Parse(text);
            });

            // Assert.
            Assert.Throws<ScriptParserException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_With_Empty_Path_04()
        {
            // Arrange.
            var text = "$var <= /\"\"/\"\"/\"\"";

            // Act.
            var act = new Action(() =>
            {
                _parser.Parse(text);
            });

            // Assert.
            Assert.Throws<ScriptParserException>(act);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Parent_Child_Parent()
        {
            // Arrange.
            var text = "/Person/Does/John\\";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(1).Single());
            var path = sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().Single();

            Assert.Equal(7, path.Parts.Length);
            Assert.IsType<ChildrenPathSubjectPart>(path.Parts.Skip(6).Single());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Parent_Child_Parent_With_Condition_bool()
        {
            // Arrange.
            var text = "/Person/Does/.IsMale=true\\";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(1).Single());
            var path = sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().Single();

            Assert.Equal(7, path.Parts.Length);
            Assert.IsType<ChildrenPathSubjectPart>(path.Parts.Skip(6).Single());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Parent_Child_Parent_With_Condition_DateTime()
        {
            // Arrange.
            var text = "/Person/Does/.Birthdate=1980-03-22\\";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(1).Single());
            var path = sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().Single();

            Assert.Equal(7, path.Parts.Length);
            Assert.IsType<ChildrenPathSubjectPart>(path.Parts.Skip(6).Single());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Parent_Child_Parent_With_Condition_Float()
        {
            // Arrange.
            var text = "/Person/Does/.Weight=76.23\\";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(1).Single());
            var path = sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().Single();

            Assert.Equal(7, path.Parts.Length);
            Assert.IsType<ChildrenPathSubjectPart>(path.Parts.Skip(6).Single());
        }

    }
}
