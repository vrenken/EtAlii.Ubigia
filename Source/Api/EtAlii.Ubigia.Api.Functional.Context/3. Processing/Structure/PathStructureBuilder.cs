namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;

    internal class PathStructureBuilder : IPathStructureBuilder
    {
        private readonly ITraversalContext _traversalContext;

        public PathStructureBuilder(ITraversalContext traversalContext)
        {
            _traversalContext = traversalContext;
        }

        public async Task Build(
            SchemaExecutionScope executionScope,
            FragmentMetadata fragmentMetadata,
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
            }

            if (path != null)
            {
                var script = new Script(new Sequence(new SequencePart[] {path}));
                var scriptResult = await _traversalContext.Process(script, executionScope.ScriptScope);

                var onlyOneSingleNode = annotation is SelectSingleNodeAnnotation ||
                                        annotation is AddAndSelectSingleNodeAnnotation ||
                                        annotation is RemoveAndSelectSingleNodeAnnotation ||
                                        annotation is LinkAndSelectSingleNodeAnnotation ||
                                        annotation is UnlinkAndSelectSingleNodeAnnotation;

                if (onlyOneSingleNode)
                {
                    if (await scriptResult.Output.SingleOrDefaultAsync() is IInternalNode lastOutput)
                    {
                        Build(lastOutput, structureName, fragmentMetadata, parent);
                    }
                }
                else
                {
                    var items = scriptResult.Output
                        .OfType<IInternalNode>()
                        .ToAsyncEnumerable()
                        .ConfigureAwait(false);
                    await foreach (var item in items)
                    {
                        Build(item, structureName, fragmentMetadata, parent);
                    }
                }
            }
        }

        private void Build(
            IInternalNode node,
            string structureName,
            FragmentMetadata fragmentMetadata,
            Structure parent)
        {
            var item = new Structure(structureName, node.Type, parent, node);
            fragmentMetadata.Items.Add(item);
        }
    }
}
