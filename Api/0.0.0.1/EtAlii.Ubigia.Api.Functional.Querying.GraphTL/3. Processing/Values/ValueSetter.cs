namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Structure;

    internal class ValueSetter : IValueSetter
    {
        private readonly IPropertiesValueSetter _propertiesValueSetter;
        private readonly IPathValueSetter _pathValueSetter;
        private readonly ISelector<Annotation, Func<Annotation, PathSubject, string, object, Structure, QueryExecutionScope, Task<Value>>> _selector;

        public ValueSetter(IPropertiesValueSetter propertiesValueSetter, IPathValueSetter pathValueSetter)
        {
            _propertiesValueSetter = propertiesValueSetter;
            _pathValueSetter = pathValueSetter;
            
            _selector = new Selector<Annotation, Func<Annotation, PathSubject, string, object, Structure, QueryExecutionScope, Task<Value>>>()
                // No traversal, just set a property.
                .Register(annotation => annotation == null, (annotation, path, valueName, value, structure, executionScope) => _propertiesValueSetter.Set(valueName, structure, value, executionScope))
                // @value(\#LastName) traversal set, i.e. a path to another node.
                .Register(annotation => annotation.Path != null, (annotation, path, valueName, value, structure, executionScope) => _pathValueSetter.Set(valueName, (string)value, structure, path, executionScope))
                // @value() traversal set, i.e. no path but the node itself.
                .Register(annotation => true, (annotation, path, valueName, value, structure, executionScope) => Task.FromResult(new Value(valueName, structure.Node.Type)));     

        }
        public async Task<Value> Set(string valueName, object value, Annotation annotation, QueryExecutionScope executionScope, Structure structure)
        {
            var setter = _selector.Select(annotation);
            return await setter(annotation, annotation?.Path, valueName, value, structure, executionScope);
        }
    }
}