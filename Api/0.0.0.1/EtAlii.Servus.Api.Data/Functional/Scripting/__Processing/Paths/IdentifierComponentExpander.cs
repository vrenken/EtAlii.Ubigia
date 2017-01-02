namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class IdentifierComponentExpander : IPathComponentExpander
    {
        public bool CanExpand(PathComponent pathComponent, int index)
        {
            return pathComponent is IdentifierComponent && index == 0;
        }

        public void Expand(PathComponent pathComponent, int index, List<string> path, ref Identifier startIdentifier)
        {
            startIdentifier = ((IdentifierComponent)pathComponent).Id;

            if (startIdentifier == Identifier.Empty)
            {
                var message = String.Format("Unable to expand identifier component in path: {0} (component: {1})", path.ToString(), pathComponent.ToString());
                throw new InvalidOperationException(message);
            }
        }
    }
}