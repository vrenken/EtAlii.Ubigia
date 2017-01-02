namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;

    public interface IPathComponentExpander
    {
        bool CanExpand(PathComponent component, int index);
        void Expand(PathComponent pathComponent, int index, List<string> path, ref Identifier startIdentifier);
    }
}