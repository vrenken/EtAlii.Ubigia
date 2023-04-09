// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System.Collections.Generic;
using System.Linq;
using EtAlii.Ubigia.Api.Functional.Traversal;

internal class PathCorrecter : IPathCorrecter
{
    public PathSubject Correct(NodeAnnotation annotation, PathSubject path)
    {
        return annotation switch
        {
            AddAndSelectMultipleNodesAnnotation a => CorrectAddition(annotation, path, a.Identity),
            AddAndSelectSingleNodeAnnotation a => CorrectAddition(annotation, path, a.Identity),
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

    private PathSubject CorrectAddition(NodeAnnotation annotation, PathSubject path, NodeIdentity identity)
    {
        var parts = BuildCorrectedPathParts(annotation, path);

        if (parts.Last() is not ParentPathSubjectPart)
        {
            parts = parts
                .Concat(new [] { new ParentPathSubjectPart() })
                .ToArray();
        }

        parts = parts
            .Concat(new [] { new ConstantPathSubjectPart(identity.Name) })
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
                if (first is not ParentPathSubjectPart && first is not ChildrenPathSubjectPart &&
                    last is not ParentPathSubjectPart && last is not ChildrenPathSubjectPart)
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
            if (last2 is not ParentPathSubjectPart && last2 is not ChildrenPathSubjectPart)
            {
                // If no separator is given we assume a parent 2 child relation is requested.
                correctedParts.Add(new ParentPathSubjectPart());
            }
        }

        return correctedParts.ToArray();
    }

}
