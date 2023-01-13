// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional.Tests;

using System;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public sealed class ContentRepositoryTests : IClassFixture<FunctionalInfrastructureUnitTestContext>
{
    private readonly FunctionalInfrastructureUnitTestContext _testContext;
    private readonly InfrastructureTestHelper _infrastructureTestHelper = new();

    public ContentRepositoryTests(FunctionalInfrastructureUnitTestContext testContext)
    {
        _testContext = testContext;
    }

    [Fact]
    public async Task ContentRepository_Store_Content()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var entry = await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);
        var content = _testContext.TestContentFactory.Create();

        // Act.
        await _testContext.Functional.Content.Store(entry.Id, content).ConfigureAwait(false);

        // Assert.
        Assert.True(content.Stored);
    }

    [Fact]
    public async Task ContentRepository_Store_ContentPart()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var entry = await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);
        var data = _testContext.TestContentFactory.CreateData(100, 500);
        var contentDefinition = _testContext.TestContentDefinitionFactory.Create(data);
        await _testContext.Functional.ContentDefinition.Store(entry.Id, contentDefinition).ConfigureAwait(false);
        var content = _testContext.TestContentFactory.Create(1);
        var contentPart = _testContext.TestContentFactory.CreatePart(data);

        // Act.
        await _testContext.Functional.Content.Store(entry.Id, content).ConfigureAwait(false);
        await _testContext.Functional.Content.Store(entry.Id, contentPart).ConfigureAwait(false);

        // Assert.
        Assert.True(content.Stored);
        Assert.True(contentPart.Stored);
    }


    [Fact]
    public async Task ContentRepository_Store_ContentPart_Out_Of_Bounds()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var entry = await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);
        var content = _testContext.TestContentFactory.Create(3);
        var contentPart = _testContext.TestContentFactory.CreatePart(6);
        await _testContext.Functional.Content.Store(entry.Id, content).ConfigureAwait(false);

        // Act.
        var act = new Func<Task>(async () => await _testContext.Functional.Content.Store(entry.Id, contentPart).ConfigureAwait(false));

        // Assert.
        await Assert.ThrowsAsync<ContentRepositoryException>(act).ConfigureAwait(false);
    }

    [Fact]
    public async Task ContentRepository_Store_ContentPart_Before_Content()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var entry = await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);
        var content = _testContext.TestContentFactory.Create(1);
        var contentPart = _testContext.TestContentFactory.CreatePart(0);

        // Act.
        var act = new Func<Task>(async () => await _testContext.Functional.Content.Store(entry.Id, contentPart).ConfigureAwait(false));

        // Assert.
        Assert.NotNull(content);
        await Assert.ThrowsAsync<ContentRepositoryException>(act).ConfigureAwait(false);
    }

    [Fact]
    public async Task ContentRepository_Retrieve_Content()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var entry = await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);
        var data = _testContext.TestContentFactory.CreateData(100, 500);
        var contentDefinition = _testContext.TestContentDefinitionFactory.Create(data);
        await _testContext.Functional.ContentDefinition.Store(entry.Id, contentDefinition).ConfigureAwait(false);

        var content = _testContext.TestContentFactory.Create(1);
        var contentPart = _testContext.TestContentFactory.CreatePart(data);

        // Act.
        await _testContext.Functional.Content.Store(entry.Id, content).ConfigureAwait(false);
        await _testContext.Functional.Content.Store(entry.Id, contentPart).ConfigureAwait(false);
        var retrievedContentPart = await _testContext.Functional.Content.Get(entry.Id, 0).ConfigureAwait(false);

        // Assert.
        Assert.True(_testContext.ContentComparer.AreEqual(contentPart, retrievedContentPart));
    }

    [Fact]
    public async Task ContentRepository_Store_ContentDefinition_Null_Content()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var entry = await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);

        // Act.
        var act = new Func<Task>(async () => await _testContext.Functional.Content.Store(entry.Id, (Content) null).ConfigureAwait(false));

        // Assert.
        await Assert.ThrowsAsync<ContentRepositoryException>(act).ConfigureAwait(false);
    }

    [Fact]
    public async Task ContentRepository_Store_ContentDefinition_No_Identifier()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var entry = await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);
        var content = _testContext.TestContentFactory.Create();

        // Act.
        var act = new Func<Task>(async () => await _testContext.Functional.Content.Store(Identifier.Empty, content).ConfigureAwait(false));

        // Assert.
        Assert.NotNull(entry);
        await Assert.ThrowsAsync<ContentRepositoryException>(act).ConfigureAwait(false);
    }

    [Fact]
    public async Task ContentRepository_Get_Content()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var entry = await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);
        var content = _testContext.TestContentFactory.Create();

        // Act.
        await _testContext.Functional.Content.Store(entry.Id, content).ConfigureAwait(false);
        var retrievedContent = await _testContext.Functional.Content.Get(entry.Id).ConfigureAwait(false);

        // Assert.
        Assert.True(_testContext.ContentComparer.AreEqual(content, retrievedContent, true));
    }
}
