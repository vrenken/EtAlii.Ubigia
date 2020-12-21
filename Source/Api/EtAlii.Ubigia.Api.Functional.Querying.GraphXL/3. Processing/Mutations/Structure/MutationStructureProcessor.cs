namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    internal class MutationStructureProcessor : IMutationStructureProcessor
    {
        private readonly IRelatedIdentityFinder _relatedIdentityFinder;
        private readonly ITraversalScriptContext _scriptContext;
        private readonly IPathStructureBuilder _pathStructureBuilder;
        private readonly IPathDeterminer _pathDeterminer;
        private readonly IPathCorrecter _pathCorrecter;

        public MutationStructureProcessor(
            IRelatedIdentityFinder relatedIdentityFinder,
            ITraversalScriptContext scriptContext,
            IPathStructureBuilder pathStructureBuilder,
            IPathDeterminer pathDeterminer,
            IPathCorrecter pathCorrecter)
        {
            _relatedIdentityFinder = relatedIdentityFinder;
            _scriptContext = scriptContext;
            _pathStructureBuilder = pathStructureBuilder;
            _pathDeterminer = pathDeterminer;
            _pathCorrecter = pathCorrecter;
        }

        public async Task Process(
            StructureFragment fragment,
            FragmentMetadata fragmentMetadata,
            SchemaExecutionScope executionScope,
            IObserver<Structure> schemaOutput)
        {
            var annotation = fragment.Annotation;

            if (fragmentMetadata.Parent != null)
            {
                foreach (var structure in fragmentMetadata.Parent.Items)
                {
                    var id = _relatedIdentityFinder.Find(structure);
                    await Build(executionScope, fragmentMetadata, schemaOutput, annotation, id, fragment.Name, structure).ConfigureAwait(false);
                }
            }
            else
            {
                var id = Identifier.Empty;
                await Build(executionScope, fragmentMetadata, schemaOutput, annotation, id, fragment.Name, null).ConfigureAwait(false);
            }
        }

        private async Task Build(
            SchemaExecutionScope executionScope,
            FragmentMetadata fragmentMetadata,
            IObserver<Structure> schemaOutput,
            NodeAnnotation annotation,
            Identifier id,
            string structureName,
            Structure parent)
        {
            var path = _pathDeterminer.Determine(fragmentMetadata, annotation, id);

            var mutationScript = CreateMutationScript(annotation, path);
            if (mutationScript != null)
            {
                var scriptResult = await _scriptContext.Process(mutationScript, executionScope.ScriptScope);
                await scriptResult.Output;

                // For some operators we need to correct the path as well.
                path = _pathCorrecter.Correct(annotation, path);
            }

            await _pathStructureBuilder.Build(executionScope, fragmentMetadata, schemaOutput, annotation, structureName, parent, path).ConfigureAwait(false);

        }

        private Script CreateMutationScript(NodeAnnotation annotation, PathSubject pathSubject)
        {
            switch (annotation)
            {
                case AddAndSelectMultipleNodesAnnotation addAnnotation:
                    return new Script(new Sequence(new SequencePart[] {pathSubject, new AddOperator(), new StringConstantSubject(addAnnotation.Name) }));
                case AddAndSelectSingleNodeAnnotation addAnnotation:
                    return new Script(new Sequence(new SequencePart[] {pathSubject, new AddOperator(), new StringConstantSubject(addAnnotation.Name) }));
                case LinkAndSelectMultipleNodesAnnotation linkAnnotation:
                    return CreateLinkScript(pathSubject, linkAnnotation.Source, linkAnnotation.Target, linkAnnotation.TargetLink);
                case LinkAndSelectSingleNodeAnnotation linkAnnotation:
                    return CreateLinkScript(pathSubject, linkAnnotation.Source, linkAnnotation.Target, linkAnnotation.TargetLink);
                case RemoveAndSelectMultipleNodesAnnotation removeAnnotation:
                    return new Script(new Sequence(new SequencePart[] {pathSubject, new RemoveOperator(), new StringConstantSubject(removeAnnotation.Name) }));
                case RemoveAndSelectSingleNodeAnnotation removeAnnotation:
                    return new Script(new Sequence(new SequencePart[] {pathSubject, new RemoveOperator(), new StringConstantSubject(removeAnnotation.Name) }));
                case UnlinkAndSelectMultipleNodesAnnotation unlinkAnnotation:
                    return new Script(new []
                    {
                        new Sequence(new SequencePart[] {pathSubject, new RemoveOperator(), unlinkAnnotation.Source }),
                        new Sequence(new SequencePart[] {pathSubject, unlinkAnnotation.Source, new RemoveOperator(), unlinkAnnotation.Target }),
                        new Sequence(new SequencePart[] {unlinkAnnotation.Target, new RemoveOperator(), unlinkAnnotation.TargetLink }),
                        new Sequence(new SequencePart[] {unlinkAnnotation.Target, unlinkAnnotation.TargetLink, new RemoveOperator(), pathSubject }),
                    });
                case UnlinkAndSelectSingleNodeAnnotation unlinkAnnotation:
                    return new Script(new []
                    {
                        new Sequence(new SequencePart[] {pathSubject, new RemoveOperator(), unlinkAnnotation.Source }),
                        new Sequence(new SequencePart[] {pathSubject, unlinkAnnotation.Source, new RemoveOperator(), unlinkAnnotation.Target }),
                        new Sequence(new SequencePart[] {unlinkAnnotation.Target, new RemoveOperator(), unlinkAnnotation.TargetLink }),
                        new Sequence(new SequencePart[] {unlinkAnnotation.Target, unlinkAnnotation.TargetLink, new RemoveOperator(), pathSubject }),
                    });
                default:
                    return null;
            }
        }

        private Script CreateLinkScript(PathSubject pathSubject, PathSubject source, PathSubject target, PathSubject targetLink)
        {
            var relativeSource = new RelativePathSubject(source.Parts);
            var absoluteSourceLink = new AbsolutePathSubject(pathSubject.Parts.Concat(relativeSource.Parts).ToArray());
            var relativeTarget = new RelativePathSubject(targetLink.Parts);
            var absoluteTargetLink = new AbsolutePathSubject(target.Parts.Concat(relativeSource.Parts).ToArray());
            return new Script(new []
            {
                new Sequence(new SequencePart[] {pathSubject, new AddOperator(), relativeSource }),
                new Sequence(new SequencePart[] {absoluteSourceLink, new AddOperator(), target }),
                new Sequence(new SequencePart[] {target, new AddOperator(), relativeTarget }),
                new Sequence(new SequencePart[] {absoluteTargetLink, new AddOperator(), pathSubject }),
            });

        }

    }
}
