﻿// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;
    using Xunit;


    
    public class SequenceParserPathsWildcardTests : IDisposable
    {
        private ISequenceParser _parser;

        public SequenceParserPathsWildcardTests()
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
        public void SequenceParser_Parse_PathSubject_Wildcard_01()
        {
            // Arrange.
            var text = "/First/Second/*";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.True(pathSubject.Parts.Skip(5).Cast<WildcardPathSubjectPart>().First().Pattern == "*");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_02()
        {
            // Arrange.
            var text = "/First/*/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.True(pathSubject.Parts.Skip(3).Cast<WildcardPathSubjectPart>().First().Pattern == "*");
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_03()
        {
            // Arrange.
            var text = "/First/Second/Thi*";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.True(pathSubject.Parts.Skip(5).Cast<WildcardPathSubjectPart>().First().Pattern == "Thi*");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_04()
        {
            // Arrange.
            var text = "/First/Sec*/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.True(pathSubject.Parts.Skip(3).Cast<WildcardPathSubjectPart>().First().Pattern == "Sec*");
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_05()
        {
            // Arrange.
            var text = "/First/Second/*ird";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.True(pathSubject.Parts.Skip(5).Cast<WildcardPathSubjectPart>().First().Pattern == "*ird");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_06()
        {
            // Arrange.
            var text = "/First/*ond/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.True(pathSubject.Parts.Skip(3).Cast<WildcardPathSubjectPart>().First().Pattern == "*ond");
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_07()
        {
            // Arrange.
            var text = "/First/Second/'Thi'*";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.True(pathSubject.Parts.Skip(5).Cast<WildcardPathSubjectPart>().First().Pattern == "Thi*");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_08()
        {
            // Arrange.
            var text = "/First/'Sec'*/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.True(pathSubject.Parts.Skip(3).Cast<WildcardPathSubjectPart>().First().Pattern == "Sec*");
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_09()
        {
            // Arrange.
            var text = "/First/Second/*'ird'";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.True(pathSubject.Parts.Skip(5).Cast<WildcardPathSubjectPart>().First().Pattern == "*ird");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_10()
        {
            // Arrange.
            var text = "/First/*'ond'/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.True(pathSubject.Parts.Skip(3).Cast<WildcardPathSubjectPart>().First().Pattern == "*ond");
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        }



        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_11()
        {
            // Arrange.
            var text = "/First/Second/\"Thi\"*";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.True(pathSubject.Parts.Skip(5).Cast<WildcardPathSubjectPart>().First().Pattern == "Thi*");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_12()
        {
            // Arrange.
            var text = "/First/\"Sec\"*/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.True(pathSubject.Parts.Skip(3).Cast<WildcardPathSubjectPart>().First().Pattern == "Sec*");
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_13()
        {
            // Arrange.
            var text = "/First/Second/*\"ird\"";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.True(pathSubject.Parts.Skip(5).Cast<WildcardPathSubjectPart>().First().Pattern == "*ird");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_14()
        {
            // Arrange.
            var text = "/First/*\"ond\"/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.NotNull(sequence);
            Assert.True(sequence.Parts.Length == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
            Assert.NotNull(pathSubject);
            Assert.Equal(6, pathSubject.Parts.Length);
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(0));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(2));
            Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.True(pathSubject.Parts.Skip(3).Cast<WildcardPathSubjectPart>().First().Pattern == "*ond");
            Assert.IsType<IsParentOfPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        }

    }
}