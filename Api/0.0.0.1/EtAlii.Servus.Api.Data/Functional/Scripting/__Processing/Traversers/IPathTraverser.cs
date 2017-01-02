namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;

    public interface IPathTraverser
    {
        IReadOnlyEntry Traverse(IEnumerable<string> path, Identifier startIdentifier);
    }
}