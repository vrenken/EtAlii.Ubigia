namespace EtAlii.Ubigia.Api.Functional.Context.Analyzers.Tests
{
    using Xunit;

    public class UserPocoTests
    {
        [Fact]
        public void UserPoco_Create()
        {
            // Arrange.

            // Act.
            var poco = new UserPocoObjectGenerated();

            // Assert.
            Assert.NotNull(poco);
        }

        [Fact]
        public void UserPoco_Call_Test_Method()
        {
            // Arrange.
            var poco = new UserPocoObjectGenerated();

            // Act.
            var firstName = poco.FirstName;
            var lastName = poco.LastName;

            // Assert.
            Assert.NotNull(poco);
            Assert.Null(firstName);
            Assert.Null(lastName);
        }
    }
}
