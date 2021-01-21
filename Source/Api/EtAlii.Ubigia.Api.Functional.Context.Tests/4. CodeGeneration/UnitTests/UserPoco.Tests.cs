namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using Xunit;

    public class UserPocoTests
    {
        [Fact]
        public void UserPoco_Create()
        {
            // Arrange.

            // Act.
            var poco = new UserPocoObject();

            // Assert.
            Assert.NotNull(poco);
        }

        [Fact]
        public void UserPoco_Call_Test_Method()
        {
            // Arrange.
            var poco = new UserPocoObject();

            // Act.
            poco.GeneratedMethod();

            // Assert.
            Assert.NotNull(poco);
        }

    }
}
