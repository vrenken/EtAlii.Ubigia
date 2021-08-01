// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using Xunit;
    using EtAlii.Ubigia.Api.Functional.Context.Tests.Model;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
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
