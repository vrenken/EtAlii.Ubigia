namespace EtAlii.Ubigia.Api.Functional
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Scripting;

    internal class PathCorrecter : IPathCorrecter
    {
        public PathSubject Correct(NodeAnnotation annotation, PathSubject path)
        {
            return annotation switch
            {
                AddAndSelectMultipleNodesAnnotation a => CorrectAddition(annotation, path, a.Name),
                AddAndSelectSingleNodeAnnotation a => CorrectAddition(annotation, path, a.Name),
                RemoveAndSelectMultipleNodesAnnotation _ => CorrectSelection(annotation, path),
                RemoveAndSelectSingleNodeAnnotation _ => CorrectSelection(annotation, path),
                _ => path
            };
        }

        private PathSubject CorrectSelection(NodeAnnotation annotation, PathSubject path)
        {
            var parts = BuildCorrectedPathParts(annotation, path);
            path = path switch
            {
                RootedPathSubject rootedPathSubject => new RootedPathSubject(rootedPathSubject.Root, parts),
                AbsolutePathSubject _ => new AbsolutePathSubject(parts),
                _ => path
            };
            return path;
        }

        private PathSubject CorrectAddition(NodeAnnotation annotation, PathSubject path, string name)
        {
            var parts = BuildCorrectedPathParts(annotation, path);

            parts = parts
                .Concat(new [] { new ParentPathSubjectPart() })
                .Concat(new [] { new ConstantPathSubjectPart(name) })
                .ToArray();

            path = path switch
            {
                RootedPathSubject rootedPathSubject => new RootedPathSubject(rootedPathSubject.Root, parts),
                AbsolutePathSubject _ => new AbsolutePathSubject(parts),
                _ => path
            };

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
//                [
//                    correctedParts.AddRange(new PathSubjectPart[] { new ParentPathSubjectPart(), new ConstantPathSubjectPart(stringConstantSubject.Value)})
//                ]
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