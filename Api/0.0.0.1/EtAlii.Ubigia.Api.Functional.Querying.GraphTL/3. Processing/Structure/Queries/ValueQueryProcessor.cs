namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class ValueQueryProcessor : IValueQueryProcessor
    {
        private readonly IGraphSLScriptContext _scriptContext;

        public ValueQueryProcessor(IGraphSLScriptContext scriptContext)
        {
            _scriptContext = scriptContext;
        }

        private Identifier FindRelatedIdentifier(Structure structure)
        {
            var node = structure.Node;
            if (node != null)
            {
                return node.Id;
            }
            var parent = structure.Parent;
            return parent != null 
                ? FindRelatedIdentifier(parent) 
                : Identifier.Empty;
        }
        
        private async Task ProcessFromAnnotation(
            ValueQuery valueQuery, 
            QueryExecutionScope executionScope, 
            Structure structure)
        {
                if (valueQuery.Annotation?.Path is PathSubject path) // @value(\#LastName)
                {
                    var id = FindRelatedIdentifier(structure);
                    if (id != Identifier.Empty)
                    {
                        var parts = new PathSubjectPart[] { new ParentPathSubjectPart(), new IdentifierPathSubjectPart(id) }.Concat(path.Parts).ToArray(); 
                        path = new AbsolutePathSubject(parts); 
                        var script = new Script(new Sequence(new SequencePart[] { path }));

                        var processResult = await _scriptContext.Process(script, executionScope.ScriptScope);
                        var result = await processResult.Output.SingleOrDefaultAsync(); 
                        if (result is IInternalNode valueNode)
                        {
                            structure.EditableValues.Add(new Value(valueQuery.Name, valueNode.Type));                         
                        }
                    }
                }
                else // @value()
                {
                    structure.EditableValues.Add(new Value(valueQuery.Name, structure.Node.Type));                         
                }
        }

        private void ProcessFromValue(ValueQuery valueQuery, Structure structure)
        {
            var properties = (IPropertyDictionary)structure.Node.GetProperties();
            if (properties.TryGetValue(valueQuery.Name, out var value))
            {
                structure.EditableValues.Add(new Value(valueQuery.Name, value));                        
            }
        }

        public async Task Process(
            QueryFragment fragment, 
            QueryExecutionScope executionScope, 
            FragmentMetadata fragmentMetadata, 
            IObserver<Structure> fragmentOutput)
        {
            var valueQuery = (ValueQuery) fragment;

            if (valueQuery.Annotation != null)
            {
                foreach (var structure in fragmentMetadata.Parent.Items)
                {
                    await ProcessFromAnnotation(valueQuery, executionScope, structure);
                }
            }
            else
            {
                foreach (var structure in fragmentMetadata.Parent.Items)
                {
                    ProcessFromValue(valueQuery, structure);
                }
            }
        }
    }
}