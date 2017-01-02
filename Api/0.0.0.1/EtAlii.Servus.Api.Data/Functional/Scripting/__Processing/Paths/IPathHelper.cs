namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;

    public interface IPathHelper
    {
        IReadOnlyEntry GetEntry(Path path);
        DynamicNode Get(Path path);
        IEnumerable<DynamicNode> GetChildren(Path path);
    }
}