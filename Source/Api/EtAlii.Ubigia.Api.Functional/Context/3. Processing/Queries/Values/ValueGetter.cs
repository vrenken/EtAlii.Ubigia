// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System.Threading.Tasks;

/// <inheritdoc />
internal class ValueGetter : IValueGetter
{
    private readonly IPropertiesValueGetter _propertiesValueGetter;
    private readonly IPathValueGetter _pathValueGetter;

    public ValueGetter(
        IPropertiesValueGetter propertiesValueGetter,
        IPathValueGetter pathValueGetter)
    {
        _propertiesValueGetter = propertiesValueGetter;
        _pathValueGetter = pathValueGetter;
    }

    /// <inheritdoc />
    public async Task<Value> Get(
        string valueName,
        ValueAnnotation annotation,
        ExecutionScope scope,
        Structure structure)
    {
        if (annotation == null)
        {
            // No traversal, just get a property.
            return _propertiesValueGetter.Get(valueName, structure);
        }
        if (annotation.Source != null)
        {
            // @value(\#LastName) traversal, i.e. a path to another node.
            return await _pathValueGetter.Get(valueName, structure, annotation.Source, scope).ConfigureAwait(false);
        }
        // @value() traversal, i.e. no path but the node itself.
        return new Value(valueName, structure.Node.Type);
    }
}
