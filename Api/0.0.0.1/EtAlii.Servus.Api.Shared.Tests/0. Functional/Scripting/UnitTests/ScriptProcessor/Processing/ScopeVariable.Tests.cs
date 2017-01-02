namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ScopeVariable_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScopeVariable_New()
        {
            // Arrange.

            // Act.
            var variable = new ScopeVariable("value", "source");

            // Assert.
            Assert.AreEqual("value", await variable.Value.SingleAsync());
            Assert.AreEqual("source", variable.Source);
            Assert.AreEqual(typeof(string), await variable.Value.Select(v => v.GetType()).SingleAsync());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScopeVariable_New_From_Null()
        {
            // Arrange.

            // Act.
            var act = new Action(() =>
            {
                var variable = new ScopeVariable(null, "source");
            });

            // Assert.
            ExceptionAssert.Throws<ArgumentNullException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScopeVariable_New_From_Observable_String()
        {
            // Arrange.

            // Act.
            var variable = new ScopeVariable(Observable.Return<object>("Test"), "source");

            // Assert.
            Assert.IsNotNull(await variable.Value.SingleAsync());
            Assert.AreEqual("source", variable.Source);
            Assert.AreEqual(typeof(string), await variable.Value.Select(v => v.GetType()).SingleAsync());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScopeVariable_New_From_Observable_Int()
        {
            // Arrange.

            // Act.
            var variable = new ScopeVariable(Observable.Return<object>(1), "source");

            // Assert.
            Assert.IsNotNull(await variable.Value.SingleAsync());
            Assert.AreEqual("source", variable.Source);
            Assert.AreEqual(typeof(int), await variable.Value.Select(v => v.GetType()).SingleAsync());
        }
    }
}