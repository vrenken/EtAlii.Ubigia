// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;

    /// <inheritdoc />
    internal class PathStructureBuilder : IPathStructureBuilder
    {
        private readonly ITraversalContext _traversalContext;

        public PathStructureBuilder(ITraversalContext traversalContext)
        {
            _traversalContext = traversalContext;
        }

        /// <inheritdoc />
        public async Task Build(
            ExecutionScope scope,
            ExecutionPlanResultSink executionPlanResultSink,
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
                executionPlanResultSink.Items.Add(item);
            }

            if (path != null)
            {
                var script = new Script(new Sequence(new SequencePart[] {path}));
                var scriptResult = await _traversalContext.Process(script, scope);

                var onlyOneSingleNode = annotation is SelectSingleNodeAnnotation ||
                                        annotation is AddAndSelectSingleNodeAnnotation ||
                                        annotation is RemoveAndSelectSingleNodeAnnotation ||
                                        annotation is LinkAndSelectSingleNodeAnnotation ||
                                        annotation is UnlinkAndSelectSingleNodeAnnotation;

                if (onlyOneSingleNode)
                {
                    if (await scriptResult.Output.SingleOrDefaultAsync() is Node lastOutput)
                    {
                        Build(lastOutput, structureName, executionPlanResultSink, parent);
                    }
                }
                else
                {
                    var items = scriptResult.Output
                        .OfType<Node>()
                        .ToAsyncEnumerable()
                        .ConfigureAwait(false);
                    await foreach (var item in items)
                    {
                        Build(item, structureName, executionPlanResultSink, parent);
                    }
                }
            }
        }

        private void Build(
            Node node,
            string structureName,
            ExecutionPlanResultSink executionPlanResultSink,
            Structure parent)
        {
            var item = new Structure(structureName, node.Type, parent, node);
            executionPlanResultSink.Items.Add(item);
        }
    }
}
