﻿namespace EtAlii.Servus.Api.Functional.Tests
{
    using EtAlii.Servus.Api.Tests;
    using Xunit;


    public class DataContext_ChangeTracker_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ChangeTracker_New()
        {
            // Arrange.

            // Act.
            new ChangeTracker();

            // Assert.
        }
    }
}