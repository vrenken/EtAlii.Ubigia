﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context.Tests;

using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Functional.Tests;
using EtAlii.Ubigia.Tests;
using Xunit;

[CorrelateUnitTests]
public class QueryExecutionPlannerTests : IClassFixture<FunctionalUnitTestContext>
{
    private readonly FunctionalUnitTestContext _testContext;

    public QueryExecutionPlannerTests(FunctionalUnitTestContext testContext)
    {
        _testContext = testContext;
    }

    [Fact]
    public async Task QueryExecutionPlanner_Create()
    {
        // Arrange.

        // Act.
        var planner = await _testContext
            .CreateComponentOnNewSpace<ISchemaExecutionPlanner>()
            .ConfigureAwait(false);

        // Assert.
        Assert.NotNull(planner);
    }

    [Fact]
    public async Task QueryExecutionPlanner_Plan_Simple_00()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var parser = _testContext.CreateSchemaParser();
        var planner = await _testContext
            .CreateComponentOnNewSpace<ISchemaExecutionPlanner>()
            .ConfigureAwait(false);


        var queryText = @"
            Person = @node(person:Doe/John)
            {
                age,
                company,
                email,
                phone,
                name = @node(\#FamilyName)
                {
                    first = @node(/FirstName),
                    last = @node()
                }
            }";

        var parseResult = parser.Parse(queryText, scope);
        var query = parseResult.Schema;

        // Act.
        var executionPlans = planner.Plan(query);

        // Assert.
        var executionPlanResultSink = executionPlans.FirstOrDefault()?.ResultSink;

        Assert.NotNull(executionPlanResultSink);
        Assert.Equal("Person @node(person:Doe/John)", executionPlanResultSink.ToString());
        Assert.Equal(5, executionPlanResultSink.Children.Length);
        Assert.Equal("object age", executionPlanResultSink.Children[0].Source.ToString());
        Assert.Equal("object company", executionPlanResultSink.Children[1].Source.ToString());
        Assert.Equal("object email", executionPlanResultSink.Children[2].Source.ToString());
        Assert.Equal("object phone", executionPlanResultSink.Children[3].Source.ToString());
        Assert.Equal("name @node(\\#FamilyName)", executionPlanResultSink.Children[4].ToString());

        Assert.NotNull(executionPlans);
        Assert.Equal(8, executionPlans.Length);
        Assert.Equal("Person @node(person:Doe/John)", executionPlans[0].ToString());
        Assert.Equal("object age", executionPlans[1].ToString());
        Assert.Equal("object company", executionPlans[2].ToString());
        Assert.Equal("object email", executionPlans[3].ToString());
        Assert.Equal("object phone", executionPlans[4].ToString());
        Assert.Equal("name @node(\\#FamilyName)", executionPlans[5].ToString());
        Assert.Equal("object first @node(/FirstName)", executionPlans[6].ToString());
        Assert.Equal("object last @node()", executionPlans[7].ToString());
    }
}
