namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using Xunit;


    
    public class ScopeVariableTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScopeVariable_New()
        {
            // Arrange.

            // Act.
            var variable = new ScopeVariable("value", "source");

            // Assert.
            Assert.Equal("value", await variable.Value.SingleAsync());
            Assert.Equal("source", variable.Source);
            Assert.Equal(typeof(string), await variable.Value.Select(v => v.GetType()).SingleAsync());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScopeVariable_New_From_Null()
        {
            // Arrange.

            // Act.
            var act = new Action(() =>
            {
                var variable = new ScopeVariable(null, "source");
            });

            // Assert.
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScopeVariable_New_From_Observable_String()
        {
            // Arrange.

            // Act.
            var variable = new ScopeVariable(Observable.Return<object>("Test"), "source");

            // Assert.
            Assert.NotNull(await variable.Value.SingleAsync());
            Assert.Equal("source", variable.Source);
            Assert.Equal(typeof(string), await variable.Value.Select(v => v.GetType()).SingleAsync());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScopeVariable_New_From_Observable_Int()
        {
            // Arrange.

            // Act.
            var variable = new ScopeVariable(Observable.Return<object>(1), "source");

            // Assert.
            Assert.NotNull(await variable.Value.SingleAsync());
            Assert.Equal("source", variable.Source);
            Assert.Equal(typeof(int), await variable.Value.Select(v => v.GetType()).SingleAsync());
        }
    }
}