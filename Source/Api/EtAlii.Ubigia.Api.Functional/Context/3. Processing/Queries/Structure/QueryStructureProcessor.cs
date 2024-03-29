// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System.Threading.Tasks;

/// <inheritdoc />
internal class QueryStructureProcessor : IQueryStructureProcessor
{
    private readonly IRelatedIdentityFinder _relatedIdentityFinder;
    private readonly IPathStructureBuilder _pathStructureBuilder;
    private readonly IPathDeterminer _pathDeterminer;

    public QueryStructureProcessor(
        IRelatedIdentityFinder relatedIdentityFinder,
        IPathStructureBuilder pathStructureBuilder,
        IPathDeterminer pathDeterminer)
    {
        _relatedIdentityFinder = relatedIdentityFinder;
        _pathStructureBuilder = pathStructureBuilder;
        _pathDeterminer = pathDeterminer;
    }

    /// <inheritdoc />
    public async Task Process(
        StructureFragment fragment,
        ExecutionPlanResultSink executionPlanResultSink,
        ExecutionScope scope)
    {
        var annotation = fragment.Annotation;

        if (executionPlanResultSink.Parent != null)
        {
            foreach (var structure in executionPlanResultSink.Parent.Items)
            {
                var id = _relatedIdentityFinder.Find(structure);
                await Build(scope, executionPlanResultSink, annotation, id, fragment.Name, structure).ConfigureAwait(false);
            }
        }
        else
        {
            var id = Identifier.Empty;
            await Build(scope, executionPlanResultSink, annotation, id, fragment.Name, null).ConfigureAwait(false);
        }
    }


    private async Task Build(
        ExecutionScope scope,
        ExecutionPlanResultSink executionPlanResultSink,
        NodeAnnotation annotation,
        Identifier id,
        string structureName,
        Structure parent)
    {
        var path = _pathDeterminer.Determine(executionPlanResultSink, annotation, id);

        await _pathStructureBuilder.Build(scope, executionPlanResultSink, annotation, structureName, parent, path).ConfigureAwait(false);
    }
}
