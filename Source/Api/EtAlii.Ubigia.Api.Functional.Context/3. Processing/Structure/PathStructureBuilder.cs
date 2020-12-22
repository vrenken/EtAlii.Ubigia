namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;

    internal class PathStructureBuilder : IPathStructureBuilder
    {
        private readonly ITraversalScriptContext _scriptContext;

        public PathStructureBuilder(ITraversalScriptContext scriptContext)
        {
            _scriptContext = scriptContext;
        }

        public async Task Build(
            SchemaExecutionScope executionScope,
            FragmentMetadata fragmentMetadata,
            IObserver<Structure> schemaOutput,
            NodeAnnotation annotation,
            string structureName,
            Structure parent,
            PathSubject path)
        {
            if (path == null)
            {
                // We've got a placeholder root fragment. Let's add a single structure so that
                // other structures can be put inside of it.
                var item = new Structure(structureName, null, parent, null);
                fragmentMetadata.Items.Add(item);
                schemaOutput.OnNext(item);
                return;
            }

            var script = new Script(new Sequence(new SequencePart[] {path}));
            var scriptResult = await _scriptContext.Process(script, executionScope.ScriptScope);

            var onlyOneSingleNode = annotation is SelectSingleNodeAnnotation ||
                                    annotation is AddAndSelectSingleNodeAnnotation ||
                                    annotation is RemoveAndSelectSingleNodeAnnotation ||
                                    annotation is LinkAndSelectSingleNodeAnnotation ||
                                    annotation is UnlinkAndSelectSingleNodeAnnotation;

            if (onlyOneSingleNode)
            {
                if (await scriptResult.Output.SingleOrDefaultAsync() is IInternalNode lastOutput)
                {
                    Build(lastOutput, schemaOutput, structureName, fragmentMetadata, parent);
                }
            }
            else
            {
                scriptResult.Output
                    .OfType<IInternalNode>()
                    .Subscribe(
                        onError: schemaOutput.OnError,
                        onNext: o => Build(o, schemaOutput, structureName, fragmentMetadata, parent),
                        onCompleted: () => { });
            }
        }

        private void Build(
            IInternalNode node,
            IObserver<Structure> schemaOutput,
            string structureName,
            FragmentMetadata fragmentMetadata,
            Structure parent)
        {
            var item = new Structure(structureName, node.Type, parent, node);
            fragmentMetadata.Items.Add(item);
            schemaOutput.OnNext(item);
        }
    }
}
