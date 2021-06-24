// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
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
        public async Task<Value> Set(string valueName, object value, ValueAnnotation annotation, SchemaExecutionScope executionScope, Structure structure)
        {
            if (annotation == null)
            {
                // No traversal, just set a property.
                return await _propertiesValueSetter.Set(valueName, structure, value, executionScope).ConfigureAwait(false);
            }
            if (annotation.Source != null)
            {
                // @value(\#LastName) traversal set, i.e. a path to another node.
                return await _pathValueSetter.Set(valueName, (string) value, structure, annotation.Source, executionScope).ConfigureAwait(false);
            }
            // @value() traversal set, i.e. no path but the node itself.
            return new Value(valueName, structure.Node.Type);
        }
    }
}
