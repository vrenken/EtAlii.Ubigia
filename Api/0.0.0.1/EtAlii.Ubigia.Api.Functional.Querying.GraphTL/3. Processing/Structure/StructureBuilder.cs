namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class StructureBuilder : IStructureBuilder
    {
        private readonly IGraphSLScriptContext _scriptContext;

        public StructureBuilder(IGraphSLScriptContext scriptContext)
        {
            _scriptContext = scriptContext;
        }

        public async Task Build(
            QueryExecutionScope executionScope, 
            FragmentMetadata fragmentMetadata,
            IObserver<Structure> fragmentOutput, 
            Annotation annotation, Identifier id, 
            string structureName,
            Structure parent)
        {
            var path = annotation?.Path;
            Script script;
            
            if (id != Identifier.Empty && path != null)
            {
                // An Id and a path.
                var parts = new PathSubjectPart[] {new ParentPathSubjectPart(), new IdentifierPathSubjectPart(id)}
                    .Concat(path.Parts).ToArray();
                path = new AbsolutePathSubject(parts);
                script = new Script(new Sequence(new SequencePart[] {path}));
            }
            else if (id == Identifier.Empty && path != null)
            {
                // No Id but a path.
                script = new Script(new Sequence(new SequencePart[] {path}));
            }
            else if (id != Identifier.Empty && path == null)
            {
                // An Id but no path.
                var parts = new PathSubjectPart[] {new ParentPathSubjectPart(), new IdentifierPathSubjectPart(id)};
                path = new AbsolutePathSubject(parts);
                script = new Script(new Sequence(new SequencePart[] { path}));
            }
            else
            {
                // No Id and no path.
                throw new QueryProcessingException($"Unable to process fragment. No Id nor an annotation path found: {fragmentMetadata}");
            }
            
            var scriptResult = await _scriptContext.Process(script, executionScope.ScriptScope);

            switch (annotation?.Type)
            {
                case AnnotationType.Node:
                    if (await scriptResult.Output.SingleOrDefaultAsync() is IInternalNode lastOutput)
                    {
                        Build(lastOutput, fragmentOutput, structureName, fragmentMetadata, parent);
                    }

                    break;
                case AnnotationType.Nodes:
                case null: // We have a nested node.
                    scriptResult.Output
                        .OfType<IInternalNode>() 
                        .Subscribe(
                            onError: fragmentOutput.OnError,
                            onNext: o => Build(o, fragmentOutput, structureName, fragmentMetadata, parent),
                            onCompleted: () => { });
                    break;
            }
        }

        private void Build(
            IInternalNode node, 
            IObserver<Structure> fragmentOutput, 
            string structureName, 
            FragmentMetadata fragmentMetadata,
            Structure parent)
        {

            var item = new Structure(structureName, node.Type, parent, node);
            fragmentMetadata.Items.Add(item);
            fragmentOutput.OnNext(item);
        }
    }
}