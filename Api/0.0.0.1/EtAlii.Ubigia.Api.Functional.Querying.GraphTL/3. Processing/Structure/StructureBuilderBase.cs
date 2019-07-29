namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal abstract class StructureBuilderBase
    {
        protected readonly IGraphSLScriptContext ScriptContext;

        protected StructureBuilderBase(IGraphSLScriptContext scriptContext)
        {
            ScriptContext = scriptContext;
        }

        protected async Task BuildFromPath(
            QueryExecutionScope executionScope, 
            FragmentMetadata fragmentMetadata,
            IObserver<Structure> fragmentOutput, 
            Annotation annotation, 
            string structureName, 
            Structure parent, 
            PathSubject path)
        {
            var script = new Script(new Sequence(new SequencePart[] {path}));
            var scriptResult = await ScriptContext.Process(script, executionScope.ScriptScope);

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
                case AnnotationType.Value:
                    break;
            }
        }

        protected PathSubject DeterminePath(FragmentMetadata fragmentMetadata, Annotation annotation, Identifier id)
        {
            var path = annotation?.Path;

            if (path is RootedPathSubject rootedPath)
            {
                // A rooted path.
                path = rootedPath;
            }
            if (id != Identifier.Empty && path != null)
            {
                // An Id and a path.
                var parts = new PathSubjectPart[] {new ParentPathSubjectPart(), new IdentifierPathSubjectPart(id)}
                    .Concat(path.Parts).ToArray();
                path = new AbsolutePathSubject(parts);
            }
            else if (id == Identifier.Empty && path != null)
            {
                // No Id but a path.
            }
            else if (id != Identifier.Empty && path == null)
            {
                // An Id but no path.
                var parts = new PathSubjectPart[] {new ParentPathSubjectPart(), new IdentifierPathSubjectPart(id)};
                path = new AbsolutePathSubject(parts);
            }
            else
            {
                // No Id and no path.
                throw new QueryProcessingException($"Unable to process fragment. No Id nor an annotation path found: {fragmentMetadata}");
            }

            return path;
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