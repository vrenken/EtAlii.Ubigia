namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    internal class PathDeterminer : IPathDeterminer
    {

        public PathSubject Determine(FragmentMetadata fragmentMetadata, NodeAnnotation annotation, in Identifier id)
        {
            var path = annotation?.Source;

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
                //throw new SchemaProcessingException($"Unable to process fragment. No Id nor an annotation path found: {fragmentMetadata}")
            }

            return path;
        }

    }
}
