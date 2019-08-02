namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;

    internal class PathDeterminer : IPathDeterminer
    {
    
        public PathSubject Determine(FragmentMetadata fragmentMetadata, Annotation annotation, Identifier id)
        {
            var path = annotation?.Path;

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