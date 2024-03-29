// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Functional.Traversal;

internal class MutationStructureProcessor : IMutationStructureProcessor
{
    private readonly IRelatedIdentityFinder _relatedIdentityFinder;
    private readonly ITraversalContext _traversalContext;
    private readonly IPathStructureBuilder _pathStructureBuilder;
    private readonly IPathDeterminer _pathDeterminer;
    private readonly IPathCorrecter _pathCorrecter;
    private readonly IVariableToSubjectConverter _variableToSubjectConverter;

    public MutationStructureProcessor(
        IRelatedIdentityFinder relatedIdentityFinder,
        ITraversalContext traversalContext,
        IPathStructureBuilder pathStructureBuilder,
        IPathDeterminer pathDeterminer,
        IPathCorrecter pathCorrecter,
        IVariableToSubjectConverter variableToSubjectConverter)
    {
        _relatedIdentityFinder = relatedIdentityFinder;
        _traversalContext = traversalContext;
        _pathStructureBuilder = pathStructureBuilder;
        _pathDeterminer = pathDeterminer;
        _pathCorrecter = pathCorrecter;
        _variableToSubjectConverter = variableToSubjectConverter;
    }

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

        var mutationScript = await CreateMutationScript(annotation, path, scope).ConfigureAwait(false);
        if (mutationScript != null)
        {
            var scriptResult = await _traversalContext
                .Process(mutationScript, scope);
            await scriptResult.Output;

            // For some operators we need to correct the path as well.
            path = _pathCorrecter.Correct(annotation, path);
        }

        await _pathStructureBuilder
            .Build(scope, executionPlanResultSink, annotation, structureName, parent, path)
            .ConfigureAwait(false);

    }

    private async Task<Subject> ToSubject(NodeIdentity identity, ExecutionScope scope)
    {
        if (!identity.IsVariable)
        {
            return new StringConstantSubject(identity.Name);
        }

        if (scope.Variables.TryGetValue(identity.Name, out var variable))
        {
            return await _variableToSubjectConverter
                .Convert(variable)
                .ConfigureAwait(false);
        }
        throw new InvalidOperationException($"Variable with name '{identity.Name}' not found");
    }

    private async Task<Script> CreateMutationScript(NodeAnnotation annotation, PathSubject pathSubject, ExecutionScope scope)
    {
        switch (annotation)
        {
            case AddAndSelectMultipleNodesAnnotation addAnnotation:
                return new Script(new Sequence(new SequencePart[] {pathSubject, new AddOperator(), await ToSubject(addAnnotation.Identity, scope).ConfigureAwait(false) }));
            case AddAndSelectSingleNodeAnnotation addAnnotation:
                return new Script(new Sequence(new SequencePart[] {pathSubject, new AddOperator(), await ToSubject(addAnnotation.Identity, scope).ConfigureAwait(false) }));
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
