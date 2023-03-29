// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Tests;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Functional.Context;
using EtAlii.Ubigia.Api.Functional.Traversal;
using EtAlii.Ubigia.Api.Logical;
using EtAlii.xTechnology.MicroContainer;

public static class FunctionalUnitTestContextExtensions
{
    public static async Task<(TFirstInstance, TSecondInstance)> CreateComponentOnNewSpace<TFirstInstance, TSecondInstance>(this FunctionalUnitTestContext context)
    {
        var options = await new FunctionalOptions(context.ClientConfiguration)
            .UseTestParsing()
            .UseLogicalContext(context, true)
            .ConfigureAwait(false);

        return Factory.Create<TFirstInstance, TSecondInstance>(options);
    }

    public static async Task<TInstance> CreateComponentOnNewSpace<TInstance>(this FunctionalUnitTestContext context)
    {
        var options = await new FunctionalOptions(context.ClientConfiguration)
            .UseTestParsing()
            .UseLogicalContext(context, true)
            .ConfigureAwait(false);

        return Factory.Create<TInstance>(options);
    }

    public static ISchemaProcessor CreateSchemaProcessor(this FunctionalUnitTestContext context, ILogicalContext logicalContext)
    {
        var options = new FunctionalOptions(context.ClientConfiguration)
            .UseTestParsing()
            .UseDiagnostics()
            .UseLogicalContext(logicalContext);

        return Factory.Create<ISchemaProcessor>(options);
    }

    public static IScriptParser CreateScriptParser(this FunctionalUnitTestContext context)
    {
        var options = new FunctionalOptions(context.ClientConfiguration)
            .UseTestParsing()
            .UseDiagnostics()
            .UseLogicalContext(context);

        return Factory.Create<IScriptParser>(options);
    }

    public static IScriptProcessor CreateScriptProcessor(this FunctionalUnitTestContext context, LogicalOptions logicalOptions)
    {
        var options = logicalOptions
            .UseFunctionalContext()
            .UseTestParsing()
            .UseDiagnostics();

        return Factory.Create<IScriptProcessor>(options);
    }

    public static async Task<IScriptProcessor> CreateScriptProcessorOnNewSpace(this FunctionalUnitTestContext context)
    {
        var options = await new FunctionalOptions(context.ClientConfiguration)
            .UseTestParsing()
            .UseDiagnostics()
            .UseLogicalContext(context, true)
            .ConfigureAwait(false);

        return Factory.Create<IScriptProcessor>(options);
    }

    public static ISchemaParser CreateSchemaParser(this FunctionalUnitTestContext context)
    {
        var options = new FunctionalOptions(context.ClientConfiguration)
            .UseTestParsing()
            .UseDiagnostics()
            .UseLogicalContext(context);

        return Factory.Create<ISchemaParser>(options);
    }

    internal static async Task<ISequencePartExecutionPlannerSelector> CreateSequencePartExecutionPlannerSelector(this FunctionalUnitTestContext context)
    {
        var options = await new FunctionalOptions(context.ClientConfiguration)
            .UseTestParsing()
            .UseLogicalContext(context, true)
            .ConfigureAwait(false);

        return Factory.Create<ISequencePartExecutionPlannerSelector>(options);
    }

    internal static IExecutionPlanCombinerSelector CreateExecutionPlanCombinerSelector(this FunctionalUnitTestContext context, ISequencePartExecutionPlannerSelector sequencePartExecutionPlannerSelector)
    {
        var sourceExecutionPlanCombiner = new SubjectExecutionPlanCombiner();
        var binaryExecutionPlanCombiner = new OperatorExecutionPlanCombiner(sequencePartExecutionPlannerSelector, sourceExecutionPlanCombiner);

        return new ExecutionPlanCombinerSelector(sourceExecutionPlanCombiner, binaryExecutionPlanCombiner);
    }
}
