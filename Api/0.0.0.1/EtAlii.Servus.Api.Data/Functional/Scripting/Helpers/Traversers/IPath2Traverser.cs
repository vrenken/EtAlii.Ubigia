namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;

    public interface IPath2Traverser
    {
        IReadOnlyEntry Traverse(IEnumerable<PathSubjectPart> path);
    }
}