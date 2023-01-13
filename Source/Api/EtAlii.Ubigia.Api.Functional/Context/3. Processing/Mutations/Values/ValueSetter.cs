// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

internal class ValueSetter : IValueSetter
{
    private readonly IPropertiesValueSetter _propertiesValueSetter;
    private readonly IPathValueSetter _pathValueSetter;

    public ValueSetter(IPropertiesValueSetter propertiesValueSetter, IPathValueSetter pathValueSetter)
    {
        _propertiesValueSetter = propertiesValueSetter;
        _pathValueSetter = pathValueSetter;
    }
    public async Task<Value> Set(string valueName, object value, ValueAnnotation annotation, ExecutionScope scope, Structure structure)
    {
        if (annotation == null)
        {
            var valueToSet = await GetValueToSet(value, scope).ConfigureAwait(false);
            // No traversal, just set a property.
            return await _propertiesValueSetter.Set(valueName, structure, valueToSet, scope).ConfigureAwait(false);
        }
        if (annotation.Source != null)
        {
            var valueToSet = await GetValueToSet(value, scope).ConfigureAwait(false);

            // @value(\#LastName) traversal set, i.e. a path to another node.
            return await _pathValueSetter.Set(valueName, (string) valueToSet, structure, annotation.Source, scope).ConfigureAwait(false);
        }
        // @value() traversal set, i.e. no path but the node itself.
        return new Value(valueName, structure.Node.Type);
    }

    private async Task<object> GetValueToSet(object value, ExecutionScope scope)
    {
        object valueToSet;
        switch (value)
        {
            case PrimitiveMutationValue primitiveMutationValue:
                valueToSet = primitiveMutationValue.Value;
                break;
            case VariableMutationValue variableMutationValue:
                if (scope.Variables.TryGetValue(variableMutationValue.Name, out var variable))
                {
                    valueToSet = await variable.Value.SingleAsync();
                }
                else
                {
                    throw new InvalidOperationException($"Variable with name '{variableMutationValue.Name}' not found");
                }
                break;
            default:
                throw new InvalidOperationException("Mutation value cannot be set");
        }

        return valueToSet;
    }
}
