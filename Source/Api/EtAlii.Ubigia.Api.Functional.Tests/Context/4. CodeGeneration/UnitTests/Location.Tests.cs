namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using Xunit;
    using EtAlii.Ubigia.Api.Functional.Context.Tests.Model;

    public class LocationTests
    {
        [Fact]
        public void Location_Create()
        {
            // Arrange.

            // Act.
            var person = new Location();

            // Assert.
            Assert.NotNull(person);
        }

        [Fact]
        public void Location_With_Properties()
        {
            // Arrange.
            var location = new Location();

            // Act.
            var name = location.Name;
            var country = location.Country;
            var geoPosition = location.GeoPosition;

            // Assert.
            Assert.NotNull(location);
            Assert.Null(name);
            Assert.Null(country);
            Assert.Null(geoPosition);
        }
    }
}
