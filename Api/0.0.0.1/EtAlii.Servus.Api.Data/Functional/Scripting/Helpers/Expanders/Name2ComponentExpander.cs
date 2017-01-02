namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;

    public class Name2ComponentExpander : IPath2ComponentExpander
    {
        public bool CanExpand(PathSubjectPart part, int index)
        {
            return part is ConstantPathSubjectPart;
        }

        public void Expand(PathSubjectPart part, int index, List<PathSubjectPart> path)
        {
            path.Add((ConstantPathSubjectPart)part);
        }
    }
}