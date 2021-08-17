// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Functional.Traversal.Tests;
    using EtAlii.xTechnology.MicroContainer;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class SequenceParserPathsTraversingWildcardTests : IClassFixture<TraversalUnitTestContext>, IAsyncLifetime
    {
        private ISequenceParser _parser;
        private readonly TraversalUnitTestContext _testContext;

        public SequenceParserPathsTraversingWildcardTests(TraversalUnitTestContext testContext)
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
        public void SequenceParser_Parse_PathSubject_TraversingWildcard_01()
        {
            // Arrange.
            var text = "/First/Second/**";

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
            Assert.IsType<TraversingWildcardPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.True(pathSubject.Parts.Skip(5).Cast<TraversingWildcardPathSubjectPart>().First().Limit == 0);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_TraversingWildcard_02()
        {
            // Arrange.
            var text = "/First/**/Third";

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
            Assert.IsType<TraversingWildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.True(pathSubject.Parts.Skip(3).Cast<TraversingWildcardPathSubjectPart>().First().Limit == 0);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_TraversingWildcard_03()
        {
            // Arrange.
            var text = "/First/Second/*2*";

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
            Assert.IsType<TraversingWildcardPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.True(pathSubject.Parts.Skip(5).Cast<TraversingWildcardPathSubjectPart>().First().Limit == 2);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_TraversingWildcard_04()
        {
            // Arrange.
            var text = "/First/*100*/Third";

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
            Assert.IsType<TraversingWildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
            Assert.True(pathSubject.Parts.Skip(3).Cast<TraversingWildcardPathSubjectPart>().First().Limit == 100);
            Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
            Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_TraversingWildcard_05()
        {
            // Arrange.
            var text = "/First/Second/*-20*";

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
            Assert.IsType<TraversingWildcardPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.True(pathSubject.Parts.Skip(5).Cast<TraversingWildcardPathSubjectPart>().First().Limit == -20);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_TraversingWildcard_06()
        {
            // Arrange.
            var text = "/First/Second/*+20*";

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
            Assert.IsType<TraversingWildcardPathSubjectPart>(pathSubject.Parts.ElementAt(5));
            Assert.True(pathSubject.Parts.Skip(5).Cast<TraversingWildcardPathSubjectPart>().First().Limit == +20);
        }
    }
}
