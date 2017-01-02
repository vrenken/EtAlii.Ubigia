namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;

    public class Identifier2ComponentExpander : IPath2ComponentExpander
    {
        public bool CanExpand(PathSubjectPart part, int index)
        {
            return part is IdentifierPathSubjectPart;
        }

        public void Expand(PathSubjectPart part, int index, List<PathSubjectPart> path)
        {
            path.Add((IdentifierPathSubjectPart)part);
        }
    }
}