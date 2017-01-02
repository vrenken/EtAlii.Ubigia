namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;

    public interface IPath2Expander
    {
        IEnumerable<PathSubjectPart> Expand(PathSubject path);
    }
}