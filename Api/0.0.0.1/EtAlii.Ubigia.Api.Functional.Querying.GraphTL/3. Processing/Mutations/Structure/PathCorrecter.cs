namespace EtAlii.Ubigia.Api.Functional
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Scripting;

    internal class PathCorrecter : IPathCorrecter
    {
        public PathSubject Correct(NodeAnnotation annotation, PathSubject path)
        {
            switch (annotation)
            {
                case AddAndSelectMultipleNodesAnnotation _:
                case AddAndSelectSingleNodeAnnotation _:
                case RemoveAndSelectMultipleNodesAnnotation _:
                case RemoveAndSelectSingleNodeAnnotation _:
                //case AddOperator _
                //case RemoveOperator _:
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
                //case AssignOperator _:
                //    throw new SchemaProcessingException("Assignments cannot be done using @node/@nodes mutations. Use @value mutations instead.");
            }

            return path;
        }

        private PathSubjectPart[] BuildCorrectedPathParts(NodeAnnotation annotation, PathSubject path)
        {
            var correctedParts = new List<PathSubjectPart>(path.Parts);

            var onlyOneSingleNode = annotation is SelectSingleNodeAnnotation ||
                                    annotation is AddAndSelectSingleNodeAnnotation ||
                                    annotation is RemoveAndSelectSingleNodeAnnotation ||
                                    annotation is LinkAndSelectSingleNodeAnnotation ||
                                    annotation is UnlinkAndSelectSingleNodeAnnotation;

            if (onlyOneSingleNode)
            {
//                if (annotation.Source is StringConstantSubject stringConstantSubject)
//                {
//                    correctedParts.AddRange(new PathSubjectPart[] { new ParentPathSubjectPart(), new ConstantPathSubjectPart(stringConstantSubject.Value)});
//                }
//                else 
                if (annotation.Source is RelativePathSubject relativePathSubject)
                {
                    var last = path.Parts.Last();
                    var first = relativePathSubject.Parts.First();
                    if (!(first is ParentPathSubjectPart) && !(first is ChildrenPathSubjectPart) &&
                        !(last is ParentPathSubjectPart) && !(last is ChildrenPathSubjectPart))
                    {
                        // If no separator is given we assume a parent 2 child relation is requested.
                        //correctedParts.Add(new AllUpdatesPathSubjectPart())
                        correctedParts.Add(new ParentPathSubjectPart());
                    }
                    correctedParts.AddRange(relativePathSubject.Parts);
                }
            }
            else
            {
                var last2 = path.Parts.Last();
                if (!(last2 is ParentPathSubjectPart) && !(last2 is ChildrenPathSubjectPart))
                {
                    // If no separator is given we assume a parent 2 child relation is requested.
                    correctedParts.Add(new ParentPathSubjectPart());
                }
            }

            return correctedParts.ToArray();
        }

    }
}