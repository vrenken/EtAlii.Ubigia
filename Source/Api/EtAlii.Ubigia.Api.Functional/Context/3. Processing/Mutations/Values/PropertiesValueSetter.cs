// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;

    internal class PropertiesValueSetter : IPropertiesValueSetter
    {
        private readonly ITraversalContext _traversalContext;

        public PropertiesValueSetter(ITraversalContext traversalContext)
        {
            _traversalContext = traversalContext;
        }

        public async Task<Value> Set(string valueName, Structure structure, object value, SchemaExecutionScope executionScope)
        {
            var properties = structure.Node.Properties;
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

            var processResult = await _traversalContext.Process(script, executionScope.ScriptScope);
            var result = await processResult.Output.SingleOrDefaultAsync();
            if (result is Node valueNode)
            {
                properties = valueNode.Properties;
                return properties.TryGetValue(valueName, out var newValue)
                    ? new Value(valueName, newValue)
                    : null;
            }
            return null;
        }

    }
}
