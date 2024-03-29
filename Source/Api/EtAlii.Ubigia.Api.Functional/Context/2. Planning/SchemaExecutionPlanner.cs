// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System.Collections.Generic;

internal class SchemaExecutionPlanner : ISchemaExecutionPlanner
{
    private readonly IQueryStructureProcessor _queryStructureProcessor;
    private readonly IMutationStructureProcessor _mutationStructureProcessor;
    private readonly IQueryValueProcessor _queryValueProcessor;
    private readonly IMutationValueProcessor _mutationValueProcessor;

    public SchemaExecutionPlanner(IQueryStructureProcessor queryStructureProcessor,
        IMutationStructureProcessor mutationStructureProcessor,
        IQueryValueProcessor queryValueProcessor,
        IMutationValueProcessor mutationValueProcessor)
    {
        _queryStructureProcessor = queryStructureProcessor;
        _mutationStructureProcessor = mutationStructureProcessor;
        _queryValueProcessor = queryValueProcessor;
        _mutationValueProcessor = mutationValueProcessor;
    }

    public ExecutionPlan[] Plan(Schema schema)
    {
        var executionPlanQueue = new List<ExecutionPlan>();

        var fragment = schema.Structure;
        GetPlansForFragment(fragment, executionPlanQueue);

        return executionPlanQueue.ToArray();
    }

    private ExecutionPlanResultSink GetPlansForFragment(Fragment fragment, List<ExecutionPlan> executionPlanQueue)
    {
        var childResultSinks = new List<ExecutionPlanResultSink>();

        ExecutionPlan executionPlan;

        switch (fragment)
        {
            case ValueFragment {Type: FragmentType.Query} valueQuery:
                executionPlan = new FragmentExecutionPlan<ValueFragment>(valueQuery, _queryValueProcessor);
                executionPlanQueue.Add(executionPlan);
                break;

            case StructureFragment {Type: FragmentType.Query} structureQuery:
                executionPlan = new FragmentExecutionPlan<StructureFragment>(structureQuery, _queryStructureProcessor);
                executionPlanQueue.Add(executionPlan);
                GetPlansForChildFragments(executionPlanQueue, childResultSinks, structureQuery.Values);
                GetPlansForChildFragments(executionPlanQueue, childResultSinks, structureQuery.Children);
                break;

            case ValueFragment {Type: FragmentType.Mutation} valueMutation:
                executionPlan = new FragmentExecutionPlan<ValueFragment>(valueMutation, _mutationValueProcessor);
                executionPlanQueue.Add(executionPlan);
                break;

            case StructureFragment {Type: FragmentType.Mutation} structureMutation:
                executionPlan = new FragmentExecutionPlan<StructureFragment>(structureMutation, _mutationStructureProcessor);
                executionPlanQueue.Add(executionPlan);
                GetPlansForChildFragments(executionPlanQueue, childResultSinks, structureMutation.Values);
                break;

            default:
                var typeAsString = fragment != null ? fragment.GetType().ToString() : "NULL";
                throw new SchemaProcessingException($"Unable to plan the specified fragment type: {typeAsString}");
        }

        var resultSink = new ExecutionPlanResultSink(fragment, childResultSinks.ToArray());
        executionPlan.SetResultSink(resultSink);

        return resultSink;

    }

    private void GetPlansForChildFragments<TFragment>(
        List<ExecutionPlan> executionPlanQueue,
        List<ExecutionPlanResultSink> childResultSinks,
        TFragment[] fragments)
        where TFragment: Fragment
    {
        foreach (var fragment in fragments)
        {
            var childResultSink = GetPlansForFragment(fragment, executionPlanQueue);
            childResultSinks.Add(childResultSink);
        }
    }
}
