namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;

    public interface IPath2Helper
    {
        IReadOnlyEntry GetEntry(PathSubject path);
        DynamicNode Get(PathSubject path);
        IEnumerable<DynamicNode> GetChildren(PathSubject path);
    }
}