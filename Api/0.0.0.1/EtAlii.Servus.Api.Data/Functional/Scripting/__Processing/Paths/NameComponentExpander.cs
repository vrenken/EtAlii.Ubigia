namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;

    public class NameComponentExpander : IPathComponentExpander
    {
        public bool CanExpand(PathComponent pathComponent, int index)
        {
            return pathComponent is NameComponent;
        }

        public void Expand(PathComponent pathComponent, int index, List<string> path, ref Identifier startIdentifier)
        {
            path.Add(((NameComponent)pathComponent).Name);
        }
    }
}