// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional.Tests;

using System;
using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public class RootRepositoryTests : IClassFixture<FunctionalUnitTestContext>
{
    private readonly FunctionalUnitTestContext _testContext;
    private readonly InfrastructureTestHelper _infrastructureTestHelper = new();

    public RootRepositoryTests(FunctionalUnitTestContext testContext)
    {
        _testContext = testContext;
    }

    [Fact]
    public async Task RootRepository_Add()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var root = _infrastructureTestHelper.CreateRoot();

        // Act.
        var addedRoot = await _testContext.Functional.Roots.Add(space.Id, root).ConfigureAwait(false);

        // Assert.
        Assert.NotNull(addedRoot);
        Assert.NotEqual(addedRoot.Id, Guid.Empty);
    }

    [Fact]
    public async Task RootRepository_Get_By_Id()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var root = _infrastructureTestHelper.CreateRoot();
        var addedRoot = await _testContext.Functional.Roots.Add(space.Id, root).ConfigureAwait(false);
        Assert.NotNull(addedRoot);
        Assert.NotEqual(addedRoot.Id, Guid.Empty);

        // Act.
        var fetchedRoot = await _testContext.Functional.Roots.Get(space.Id, addedRoot.Id).ConfigureAwait(false);

        // Assert.
        Assert.Equal(addedRoot.Id, fetchedRoot.Id);
        // RCI2022: We want to make roots case insensitive.
        Assert.Equal(addedRoot.Name, fetchedRoot.Name, StringComparer.OrdinalIgnoreCase);

        Assert.Equal(root.Id, fetchedRoot.Id);
        // RCI2022: We want to make roots case insensitive.
        Assert.Equal(root.Name, fetchedRoot.Name, StringComparer.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task RootRepository_Get_By_Name()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var root = _infrastructureTestHelper.CreateRoot();
        var addedRoot = await _testContext.Functional.Roots.Add(space.Id, root).ConfigureAwait(false);
        Assert.NotNull(addedRoot);
        Assert.NotEqual(addedRoot.Id, Guid.Empty);

        // Act.
        var fetchedRoot = await _testContext.Functional.Roots.Get(space.Id, addedRoot.Name).ConfigureAwait(false);

        // Assert.
        Assert.Equal(addedRoot.Id, fetchedRoot.Id);
        // RCI2022: We want to make roots case insensitive.
        Assert.Equal(addedRoot.Name, fetchedRoot.Name, StringComparer.OrdinalIgnoreCase);

        Assert.Equal(root.Id, fetchedRoot.Id);
        // RCI2022: We want to make roots case insensitive.
        Assert.Equal(root.Name, fetchedRoot.Name, StringComparer.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task RootRepository_Remove_By_Id()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var root = _infrastructureTestHelper.CreateRoot();
        var addedRoot = await _testContext.Functional.Roots.Add(space.Id, root).ConfigureAwait(false);
        Assert.NotNull(addedRoot);
        Assert.NotEqual(addedRoot.Id, Guid.Empty);

        var fetchedRoot = await _testContext.Functional.Roots.Get(space.Id, addedRoot.Id).ConfigureAwait(false);
        Assert.NotNull(fetchedRoot);

        // Act.
        await _testContext.Functional.Roots.Remove(space.Id, addedRoot.Id).ConfigureAwait(false);

        // Assert.
        fetchedRoot = await _testContext.Functional.Roots.Get(space.Id, addedRoot.Id).ConfigureAwait(false);
        Assert.Null(fetchedRoot);
    }

    [Fact]
    public async Task RootRepository_Remove_By_Instance()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var root = _infrastructureTestHelper.CreateRoot();
        var addedRoot = await _testContext.Functional.Roots.Add(space.Id, root).ConfigureAwait(false);
        Assert.NotNull(addedRoot);
        Assert.NotEqual(addedRoot.Id, Guid.Empty);

        var fetchedRoot = await _testContext.Functional.Roots.Get(space.Id, addedRoot.Id).ConfigureAwait(false);
        Assert.NotNull(fetchedRoot);

        // Act.
        await _testContext.Functional.Roots.Remove(space.Id, addedRoot).ConfigureAwait(false);

        // Assert.
        fetchedRoot = await _testContext.Functional.Roots.Get(space.Id, addedRoot.Id).ConfigureAwait(false);
        Assert.Null(fetchedRoot);
    }

    [Fact]
    public async Task RootRepository_Get_Null()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);

        // Act.
        var root = await _testContext.Functional.Roots.Get(space.Id, Guid.NewGuid()).ConfigureAwait(false);

        // Assert.
        Assert.Null(root);
    }

    [Fact]
    public async Task RootRepository_GetAll()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var root = _infrastructureTestHelper.CreateRoot();
        var addedRoot1 = await _testContext.Functional.Roots.Add(space.Id, root).ConfigureAwait(false);
        root = _infrastructureTestHelper.CreateRoot();
        var addedRoot2 = await _testContext.Functional.Roots.Add(space.Id, root).ConfigureAwait(false);

        // Act.
        var roots = await _testContext.Functional.Roots
            .GetAll(space.Id)
            .ToArrayAsync()
            .ConfigureAwait(false);

        // Assert.
        Assert.NotNull(addedRoot1);
        Assert.NotNull(addedRoot2);
        Assert.NotNull(roots);
        Assert.True(roots.Length >= 2);
    }
}
