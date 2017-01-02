namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Path2Expander : IPath2Expander
    {
        private readonly IPath2ComponentExpander[] _pathComponentExpanders; 

        public Path2Expander(
            Identifier2ComponentExpander identifierComponentExpander,
            Variable2ComponentExpander variableComponentExpander,
            Name2ComponentExpander nameComponentExpander)
        {
            _pathComponentExpanders = new IPath2ComponentExpander[]
            {
                identifierComponentExpander,
                variableComponentExpander,
                nameComponentExpander,
            };
        }

        public IEnumerable<PathSubjectPart> Expand(PathSubject path)
        {
            var pathParts = path.Parts.ToArray();
            var expandedPath = new List<PathSubjectPart>();

            for(int i = 0; i< pathParts.Length; i++)
            {
                var pathComponent = pathParts[i];
                var pathComponentExpander = _pathComponentExpanders.SingleOrDefault(p => p.CanExpand(pathComponent, i));
                if(pathComponentExpander != null)
                {
                    pathComponentExpander.Expand(pathComponent, i, expandedPath);
                    break;
                }
                else 
                {
                    var message = String.Format("Unable to expand path: {0} (part: {1})", path.ToString(), pathComponent.ToString());
                    throw new InvalidOperationException(message);
                }
            }

            return expandedPath;
        }
    }
}
