// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System.Threading.Tasks;

/// <inheritdoc />
internal class QueryValueProcessor : IQueryValueProcessor
{
    private readonly IValueGetter _valueGetter;

    public QueryValueProcessor(IValueGetter valueGetter)
    {
        _valueGetter = valueGetter;
    }

    /// <inheritdoc />
    public async Task Process(
        ValueFragment fragment,
        ExecutionPlanResultSink executionPlanResultSink,
        ExecutionScope scope)
    {
        foreach (var structure in executionPlanResultSink.Parent.Items)
        {
            var value = await _valueGetter.Get(fragment.Name, fragment.Annotation, scope, structure).ConfigureAwait(false);
            if(value != null)
            {
                structure.EditableValues.Add(value);
            }
        }
    }
}
