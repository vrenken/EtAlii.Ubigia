namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Threading.Tasks;

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
     
        public async Task<Value> Get(
            string valueName, 
            Annotation annotation, 
            SchemaExecutionScope executionScope, 
            Structure structure)
        {
            if (annotation == null)
            {
                // No traversal, just get a property.
                return _propertiesValueGetter.Get(valueName, structure);
            }
            if (annotation.Path != null)
            {
                // @value(\#LastName) traversal, i.e. a path to another node.
                return await _pathValueGetter.Get(valueName, structure, annotation.Path, executionScope);
            }
            // @value() traversal, i.e. no path but the node itself.
            return new Value(valueName, structure.Node.Type);
        }
    }
}