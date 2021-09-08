namespace EtAlii.Ubigia.Tests
{
    using System;
    using Xunit;

    public class Base36ConvertGuidTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_Guid_ToString()
        {
            // Arrange.
            var value = Guid.Parse("716a7a7e-fdb5-4968-b26b-c2eb8b981034");

            // Act.
            var result = Base36Convert.ToString(value);

            // Assert.
            Assert.Equal("7hk7hh29c2fdxr8dobzecx5x0", result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_Guid_ToGuid()
        {
            // Arrange.
            var value = "7hk7hh29c2fdxr8dobzecx5x0";

            // Act.
            var result = Base36Convert.ToGuid(value);

            // Assert.
            Assert.Equal(Guid.Parse("716a7a7e-fdb5-4968-b26b-c2eb8b981034"), result);
        }


        // TODO: I've seen this test fail. How can we make sure it won't fail sporadically?
        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_Guid_ToString_ToGuid()
        {
            // Arrange.
            var value = Guid.NewGuid();

            // Act.
            var s = Base36Convert.ToString(value);
            var result = Base36Convert.ToGuid(s);

            // Assert.
            Assert.Equal(value, result);
        }
    }
}
