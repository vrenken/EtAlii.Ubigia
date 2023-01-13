// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System.Threading.Tasks;

internal class MutationValueProcessor : IMutationValueProcessor
{
    private readonly IValueSetter _valueSetter;

    public MutationValueProcessor(IValueSetter valueSetter)
    {
        _valueSetter = valueSetter;
    }

    public async Task Process(
        ValueFragment fragment,
        ExecutionPlanResultSink executionPlanResultSink,
        ExecutionScope scope)
    {
        foreach (var structure in executionPlanResultSink.Parent.Items)
        {
            var value = await _valueSetter
                .Set(fragment.Name, fragment.Mutation, fragment.Annotation, scope, structure)
                .ConfigureAwait(false);
            structure.EditableValues.Add(value);
        }
    }
}
