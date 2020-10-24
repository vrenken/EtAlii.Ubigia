namespace EtAlii.Ubigia.Api.Functional
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.Ubigia.Api.Logical;

    internal class PropertiesValueSetter : IPropertiesValueSetter
    {
        private readonly IGraphSLScriptContext _scriptContext;

        public PropertiesValueSetter(IGraphSLScriptContext scriptContext)
        {
            _scriptContext = scriptContext;
        }

        public async Task<Value> Set(string valueName, Structure structure, object value, SchemaExecutionScope executionScope)
        {
            var properties = structure.Node.GetProperties();
            var id = structure.Node.Id;
            
            properties[valueName] = value;
            
            var parts = new PathSubjectPart[] {new ParentPathSubjectPart(), new IdentifierPathSubjectPart(id)};
            var path = new AbsolutePathSubject(parts);
            var script = new Script(new Sequence(new SequencePart[]
            {
                path, 
                new AssignOperator(), 
                new ObjectConstantSubject(properties)
            }));

            var processResult = await _scriptContext.Process(script, executionScope.ScriptScope);
            var result = await processResult.Output.SingleOrDefaultAsync();
            if (result is IInternalNode valueNode)
            {
                properties = valueNode.GetProperties();
                return properties.TryGetValue(valueName, out var newValue)
                    ? new Value(valueName, newValue)
                    : null;
            }
            return null;
        }

    }
}