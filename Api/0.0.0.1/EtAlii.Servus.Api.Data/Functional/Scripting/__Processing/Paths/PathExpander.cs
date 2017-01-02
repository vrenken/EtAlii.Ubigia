namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class PathExpander : IPathExpander
    {
        private readonly IPathComponentExpander[] _pathComponentExpanders; 

        public PathExpander(
            IdentifierComponentExpander identifierComponentExpander,
            VariableComponentExpander variableComponentExpander,
            NameComponentExpander nameComponentExpander)
        {
            _pathComponentExpanders = new IPathComponentExpander[]
            {
                identifierComponentExpander,
                variableComponentExpander,
                nameComponentExpander,
            };
        }

        public IEnumerable<string> Expand(Path path, out Identifier startIdentifier)
        {
            startIdentifier = Identifier.Empty;

            var pathComponents = path.Components.ToArray();
            var expandedPath = new List<string>();

            for(int i = 0; i< pathComponents.Length; i++)
            {
                var pathComponent = pathComponents[i];
                var canExpand = false;
                foreach (var pathComponentExpander in _pathComponentExpanders)
                {
                    canExpand = pathComponentExpander.CanExpand(pathComponent, i);
                    if(canExpand)
                    {
                        pathComponentExpander.Expand(pathComponent, i, expandedPath, ref startIdentifier);
                        break;
                    }
                }
                if(!canExpand)
                {
                    var message = String.Format("Unable to expand path: {0} (component: {1})", path.ToString(), pathComponent.ToString());
                    throw new InvalidOperationException(message);
                }
            }

            return expandedPath;
        }
    }
}
