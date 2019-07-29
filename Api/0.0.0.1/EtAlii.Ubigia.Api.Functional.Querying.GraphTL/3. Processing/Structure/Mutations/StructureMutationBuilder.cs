namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    internal class StructureMutationBuilder : StructureBuilderBase, IStructureMutationBuilder
    {
        public StructureMutationBuilder(IGraphSLScriptContext scriptContext)
            : base(scriptContext)
        {
        }

        public async Task Build(
            QueryExecutionScope executionScope, 
            FragmentMetadata fragmentMetadata,
            IObserver<Structure> fragmentOutput, 
            Annotation annotation, 
            Identifier id, 
            string structureName,
            Structure parent)
        {
            var path = DeterminePath(fragmentMetadata, annotation, id);

            if (annotation?.Operator != null)
            {
                var mutationScript = annotation.Subject == null 
                    ? new Script(new Sequence(new SequencePart[] {path, annotation.Operator})) 
                    : new Script(new Sequence(new SequencePart[] {path, annotation.Operator, annotation.Subject}));
                var scriptResult = await ScriptContext.Process(mutationScript, executionScope.ScriptScope);
                await scriptResult.Output;

                // For some operators we need to correct the path as well.
                path = CorrectPath(annotation, path);
            }

            await BuildFromPath(executionScope, fragmentMetadata, fragmentOutput, annotation, structureName, parent, path);

        }

        private static PathSubject CorrectPath(Annotation annotation, PathSubject path)
        {
            switch (annotation.Operator)
            {
                case AddOperator _:
                case RemoveOperator _:
                    var parts = BuildCorrectedPathParts(annotation, path);
                    if (path is RootedPathSubject rootedPathSubject)
                    {
                        path = new RootedPathSubject(rootedPathSubject.Root, parts);
                    }
                    else if (path is AbsolutePathSubject)
                    {
                        path = new AbsolutePathSubject(parts);
                    }
                    break;
                case AssignOperator _:
                    throw new QueryProcessingException("Assignments cannot be done using @node/@nodes mutations. Use @value mutations instead.");
                    break;
            }

            return path;
        }

        private static PathSubjectPart[] BuildCorrectedPathParts(Annotation annotation, PathSubject path)
        {
            var correctedParts = new List<PathSubjectPart>(path.Parts);

            switch (annotation.Type)
            {
                case AnnotationType.Node:
                    if (annotation.Subject is StringConstantSubject stringConstantSubject)
                    {
                        correctedParts.AddRange(new PathSubjectPart[] { new ParentPathSubjectPart(), new ConstantPathSubjectPart(stringConstantSubject.Value)});
                    }
                    else if (annotation.Subject is RelativePathSubject relativePathSubject)
                    {
                        var last = path.Parts.Last();
                        var first = relativePathSubject.Parts.First();
                        if (!(first is ParentPathSubjectPart) && !(first is ChildrenPathSubjectPart) &&
                            !(last is ParentPathSubjectPart) && !(last is ChildrenPathSubjectPart))
                        {
                            // If no separator is given we assume a parent 2 child relation is requested.
                            correctedParts.Add(new ParentPathSubjectPart());
                        }
                        correctedParts.AddRange(relativePathSubject.Parts);
                    }
                    break;
                case AnnotationType.Nodes:
                    var last2 = path.Parts.Last();
                    if (!(last2 is ParentPathSubjectPart) && !(last2 is ChildrenPathSubjectPart))
                    {
                        // If no separator is given we assume a parent 2 child relation is requested.
                        correctedParts.Add(new ParentPathSubjectPart());
                    }
                    break;
            }

            return correctedParts.ToArray();
        }
    }
}