﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional.Tests;

using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public sealed class PropertiesRepositoryTests : IClassFixture<FunctionalUnitTestContext>
{
    private readonly FunctionalUnitTestContext _testContext;
    private readonly InfrastructureTestHelper _infrastructureTestHelper = new();

    public PropertiesRepositoryTests(FunctionalUnitTestContext testContext)
    {
        _testContext = testContext;
    }

    [Fact]
    public async Task PropertiesRepository_Store_Properties()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var entry = await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);
        var properties = _testContext.TestPropertiesFactory.Create();

        // Act.
        _testContext.Functional.Properties.Store(entry.Id, properties);

        // Assert.
        Assert.True(properties.Stored);
    }

    [Fact]
    public async Task PropertiesRepository_Retrieve_Properties()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var entry = await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);
        var properties = _testContext.TestPropertiesFactory.CreateComplete();
        _testContext.Functional.Properties.Store(entry.Id, properties);

        // Act.
        var retrievedProperties = _testContext.Functional.Properties.Get(entry.Id);

        // Assert.
        Assert.True(_testContext.PropertyDictionaryComparer.AreEqual(properties, retrievedProperties));
        Assert.True(retrievedProperties.Stored);
        Assert.True(properties.Stored);
    }
}
