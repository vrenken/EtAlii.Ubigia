namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using Xunit;
    using EtAlii.Ubigia.Api.Functional.Context.Tests.Model;

    public class PersonTests
    {
        [Fact]
        public void Person_Create()
        {
            // Arrange.

            // Act.
            var person = new Person();

            // Assert.
            Assert.NotNull(person);
        }

        [Fact]
        public void Person_With_Properties()
        {
            // Arrange.
            var person = new Person();

            // Act.
            var firstName = person.FirstName;
            var lastName = person.LastName;

            // Assert.
            Assert.NotNull(person);
            Assert.Equal("@node()", firstName);
            Assert.Equal("@node(\\#FamilyName)", lastName);
        }
    }
}
