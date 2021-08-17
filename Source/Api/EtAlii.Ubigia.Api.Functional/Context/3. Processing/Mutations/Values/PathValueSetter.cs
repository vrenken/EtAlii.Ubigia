// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;

    internal class PathValueSetter : IPathValueSetter
    {
        private readonly ITraversalContext _traversalContext;
        private readonly IRelatedIdentityFinder _relatedIdentityFinder;

        public PathValueSetter(ITraversalContext traversalContext, IRelatedIdentityFinder relatedIdentityFinder)
        {
            _traversalContext = traversalContext;
            _relatedIdentityFinder = relatedIdentityFinder;
        }

        public async Task<Value> Set(string valueName, string value, Structure structure, PathSubject path, ExecutionScope scope)
        {
            if (path is RelativePathSubject)
            {
                // If we have a relative path then we need to find out where it relates to.
                var id = _relatedIdentityFinder.Find(structure);
                if (id != Identifier.Empty)
                {
                    var parts = new PathSubjectPart[] { new ParentPathSubjectPart(), new IdentifierPathSubjectPart(id) }.Concat(path.Parts).ToArray();
                    path = new AbsolutePathSubject(parts);
                    var script = new Script(new Sequence(new SequencePart[] { path, new AssignOperator(), new StringConstantSubject(value) }));

                    var processResult = await _traversalContext.Process(script, scope);
                    var result = await processResult.Output.SingleOrDefaultAsync();
                    if (result is Node valueNode)
                    {
                        return new Value(valueName, valueNode.Type);
                    }
                }
            }
            else
            {
                // We also want to be able to set absolute or rooted paths.
                var script = new Script(new Sequence(new SequencePart[] { path, new AssignOperator(), new StringConstantSubject(value) }));
                var processResult = await _traversalContext.Process(script, scope);
                var result = await processResult.Output.SingleOrDefaultAsync();
                if (result is Node valueNode)
                {
                    return new Value(valueName, valueNode.Type);
                }
            }

            return null;
        }
    }
}
