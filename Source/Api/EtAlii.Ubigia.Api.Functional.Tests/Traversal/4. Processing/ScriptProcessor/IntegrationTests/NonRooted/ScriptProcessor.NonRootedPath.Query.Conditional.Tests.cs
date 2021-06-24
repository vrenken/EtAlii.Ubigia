// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptProcessorNonRootedPathQueryConditionalIntegrationTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly IScriptParser _parser;
        private readonly IDiagnosticsConfiguration _diagnostics;
        private readonly TraversalUnitTestContext _testContext;

        public ScriptProcessorNonRootedPathQueryConditionalIntegrationTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
            _diagnostics = DiagnosticsConfiguration.Default;
            _parser = new TestScriptParserFactory().Create();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Query_Conditional_Equals_Boolean_01()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Does",
                "/Person/Does+=John",
                "/Person/Does+=Johnny",
                "/Person/Does+=Jane",
                "/Person/Does+=Janet",
                "/Person/Does+=Joanne",
                "/Person/Does+=Joan",
                "/Person/Does/John <= { IsMale: true }",
                "/Person/Does/Johnny <= { IsMale: true }",
                "/Person/Does/Jane <= { IsMale: false }",
                "/Person/Does/Janet <= { IsMale: false }",
                "/Person/Does/Joanne <= { IsMale: false }",
                "/Person/Does/Joan <= { IsMale: false }",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Does/.IsMale=true";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(2, result.Length);
            dynamic first = result.First();
            dynamic second = result.Skip(1).First();

            Assert.Equal(true, first.IsMale);
            Assert.Equal("John", first.ToString());
            Assert.Equal(true, second.IsMale);
            Assert.Equal("Johnny", second.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Query_Conditional_Equals_Boolean_02()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Does",
                "/Person/Does+=John",
                "/Person/Does+=Johnny",
                "/Person/Does+=Jane",
                "/Person/Does+=Janet",
                "/Person/Does+=Joanne",
                "/Person/Does+=Joan",
                "/Person/Does/John <= { IsMale: true }",
                "/Person/Does/Johnny <= { IsMale: true }",
                "/Person/Does/Jane <= { IsMale: false }",
                "/Person/Does/Janet <= { IsMale: false }",
                "/Person/Does/Joanne <= { IsMale: false }",
                "/Person/Does/Joan <= { IsMale: false }",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Does/.IsMale=false";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(4, result.Length);
            dynamic first = result.First();
            dynamic second = result.Skip(1).First();
            dynamic third = result.Skip(2).First();
            dynamic fourth = result.Skip(3).First();

            Assert.Equal(false, first.IsMale);
            Assert.Equal(false, second.IsMale);
            Assert.Equal(false, third.IsMale);
            Assert.Equal(false, fourth.IsMale);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Query_Conditional_Equals_Boolean_03()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Does",
                "/Person/Does+=John",
                "/Person/Does+=Johnny",
                "/Person/Does+=Jane",
                "/Person/Does+=Janet",
                "/Person/Does+=Joanne",
                "/Person/Does+=Joan",
                "/Person/Does/John <= { IsMale: true }",
                "/Person/Does/Johnny <= { IsMale: true }",
                "/Person/Does/Jane <= { IsMale: false }",
                "/Person/Does/Janet <= { IsMale: false }",
                "/Person/Does/Joanne <= { IsMale: false }",
                "/Person/Does/Joan <= { IsMale: false }",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Does/.IsMale!=true";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(4, result.Length);
            dynamic first = result.First();
            dynamic second = result.Skip(1).First();
            dynamic third = result.Skip(2).First();
            dynamic fourth = result.Skip(3).First();

            Assert.Equal(false, first.IsMale);
            Assert.Equal(false, second.IsMale);
            Assert.Equal(false, third.IsMale);
            Assert.Equal(false, fourth.IsMale);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Query_Conditional_Equals_Boolean_04()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Does",
                "/Person/Does+=John",
                "/Person/Does+=Johnny",
                "/Person/Does+=Jane",
                "/Person/Does+=Janet",
                "/Person/Does+=Joanne",
                "/Person/Does+=Joan",
                "/Person/Does/John <= { IsMale: true }",
                "/Person/Does/Johnny <= { IsMale: true }",
                "/Person/Does/Jane <= { IsMale: false }",
                "/Person/Does/Janet <= { IsMale: false }",
                "/Person/Does/Joanne <= { IsMale: false }",
                "/Person/Does/Joan <= { IsMale: false }",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Does/.IsMale!=false";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(2, result.Length);
            dynamic first = result.First();
            dynamic second = result.Skip(1).First();

            Assert.Equal(true, first.IsMale);
            Assert.Equal("John", first.ToString());
            Assert.Equal(true, second.IsMale);
            Assert.Equal("Johnny", second.ToString());
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Query_Conditional_Equals_DateTime_01()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Does",
                "/Person/Does+=John",
                "/Person/Does+=Johnny",
                "/Person/Does+=Jane",
                "/Person/Does+=Janet",
                "/Person/Does+=Joanne",
                "/Person/Does+=Joan",
                "/Person/Does/John <= { IsMale: true, Birthdate: 1978-08-23 }",
                "/Person/Does/Johnny <= { IsMale: true }",
                "/Person/Does/Jane <= { IsMale: false, Birthdate: 1991-11-07  }",
                "/Person/Does/Janet <= { IsMale: false, Birthdate: 1976-04-09  }",
                "/Person/Does/Joanne <= { IsMale: false, Birthdate: 1978-08-23  }",
                "/Person/Does/Joan <= { IsMale: false, Birthdate: 1982-02-12  }",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Does/.Birthdate=1978-08-23";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(2, result.Length);
            dynamic first = result.First();
            dynamic second = result.Skip(1).First();

            Assert.Equal(new DateTime(1978,08, 23), first.Birthdate);
            Assert.Equal("John", first.ToString());
            Assert.Equal(new DateTime(1978, 08, 23), second.Birthdate);
            Assert.Equal("Joanne", second.ToString());
        }
    }
}
