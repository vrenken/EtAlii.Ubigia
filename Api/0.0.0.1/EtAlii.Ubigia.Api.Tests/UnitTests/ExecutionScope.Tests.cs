namespace EtAlii.Ubigia.Api.Tests
{
    using Xunit;

    public class ExecutionScopeTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ExecutionScope_Create_Cache_Enabled()
        {
            // Arrange.

            // Act.
            var executionScope = new ExecutionScope(true);

            // Assert.
            Assert.NotNull(executionScope);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ExecutionScope_Create_Cache_Disabled()
        {
            // Arrange.

            // Act.
            var executionScope = new ExecutionScope(false);

            // Assert.
            Assert.NotNull(executionScope);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ExecutionScope_GetWildCardRegex()
        {
            // Arrange.
            var executionScope = new ExecutionScope(true);

            // Act.
            var regex = executionScope.GetWildCardRegex("Vre*");

            // Assert.
            Assert.NotNull(regex);
        }

    }
}
