namespace EtAlii.Ubigia.Tests
{
    using Xunit;

    public class ExecutionScopeTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ExecutionScope_Create_Cache_Enabled()
        {
            // Arrange.

            // Act.
            var scope = new ExecutionScope();

            // Assert.
            Assert.NotNull(scope);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ExecutionScope_Create_Cache_Disabled()
        {
            // Arrange.

            // Act.
            var scope = new ExecutionScope(false);

            // Assert.
            Assert.NotNull(scope);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ExecutionScope_GetWildCardRegex()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var regex = scope.GetWildCardRegex("Vre*");

            // Assert.
            Assert.NotNull(regex);
        }

    }
}
