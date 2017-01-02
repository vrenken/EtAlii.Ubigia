namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;

    public interface IPath2ComponentExpander
    {
        bool CanExpand(PathSubjectPart part, int index);
        void Expand(PathSubjectPart part, int index, List<PathSubjectPart> path);
    }
}