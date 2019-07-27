namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Structure;

    internal class ValueGetter : IValueGetter
    {
        private readonly IPropertiesValueGetter _propertiesValueGetter;
        private readonly IPathValueGetter _pathValueGetter;
        private readonly ISelector<Annotation, Func<Annotation, PathSubject, string, Structure, QueryExecutionScope, Task<Value>>> _selector;
        
        public ValueGetter(
            IPropertiesValueGetter propertiesValueGetter, 
            IPathValueGetter pathValueGetter)
        {
            _propertiesValueGetter = propertiesValueGetter;
            _pathValueGetter = pathValueGetter;
            
            _selector = new Selector<Annotation, Func<Annotation, PathSubject, string, Structure, QueryExecutionScope, Task<Value>>>()
                // No traversal, just get a property.
                .Register(annotation => annotation == null, (annotation, path, valueName, structure, executionScope) => Task.FromResult(_propertiesValueGetter.Get(valueName, structure)))
                // @value(\#LastName) traversal, i.e. a path to another node.
                .Register(annotation => annotation.Path != null, (annotation, path, valueName, structure, executionScope) => _pathValueGetter.Get(valueName, structure, path, executionScope))
                // @value() traversal, i.e. no path but the node itself.
                .Register(annotation => true, (annotation, path, valueName, structure, executionScope) => Task.FromResult(new Value(valueName, structure.Node.Type)));     
        }
     
        public async Task<Value> Get(
            string valueName, 
            Annotation annotation, 
            QueryExecutionScope executionScope, 
            Structure structure)
        {
            var getter = _selector.Select(annotation);
            return await getter(annotation, annotation?.Path, valueName, structure, executionScope);
        }
    }
}