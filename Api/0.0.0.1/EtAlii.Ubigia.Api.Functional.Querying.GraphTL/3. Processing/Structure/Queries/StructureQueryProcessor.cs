namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class StructureQueryProcessor : IStructureQueryProcessor
    {
        private readonly IGraphSLScriptContext _scriptContext;
        private readonly IRelatedIdentityFinder _relatedIdentityFinder;

        public StructureQueryProcessor(
            IGraphSLScriptContext scriptContext, 
            IRelatedIdentityFinder relatedIdentityFinder)
        {
            _scriptContext = scriptContext;
            _relatedIdentityFinder = relatedIdentityFinder;
        }

        public async Task Process(
            QueryFragment fragment, 
            QueryExecutionScope executionScope, 
            FragmentMetadata fragmentMetadata, 
            IObserver<Structure> fragmentOutput)
        {
            var structureQuery = (StructureQuery) fragment;

            var annotation = structureQuery.Annotation;

            if (fragmentMetadata.Parent != null)
            {
                foreach (var structure in fragmentMetadata.Parent.Items)
                {
                    var id = _relatedIdentityFinder.Find(structure);
                    await AddStructure(executionScope, fragmentMetadata, fragmentOutput, annotation, id, structureQuery, structure);
                }
            }
            else
            {
                var id = Identifier.Empty; 
                await AddStructure(executionScope, fragmentMetadata, fragmentOutput, annotation, id, structureQuery, null);
            }
        }

        private async Task AddStructure(
            QueryExecutionScope executionScope, 
            FragmentMetadata fragmentMetadata,
            IObserver<Structure> fragmentOutput, 
            Annotation annotation, Identifier id, 
            StructureQuery structureQuery,
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
                        AddStructure(lastOutput, fragmentOutput, structureQuery, fragmentMetadata, parent);
                    }

                    break;
                case AnnotationType.Nodes:
                case null: // We have a nested node.
                    scriptResult.Output
                        .OfType<IInternalNode>() 
                        .Subscribe(
                            onError: fragmentOutput.OnError,
                            onNext: o => AddStructure(o, fragmentOutput, structureQuery, fragmentMetadata, parent),
                            onCompleted: () => { });
                    break;
            }
        }

        private void AddStructure(
            IInternalNode node, 
            IObserver<Structure> fragmentOutput, 
            StructureQuery structureQuery, 
            FragmentMetadata fragmentMetadata,
            Structure parent)
        {

            var item = new Structure(structureQuery.Name, node.Type, parent, node);
            fragmentMetadata.Items.Add(item);
            fragmentOutput.OnNext(item);
        }
    }
}