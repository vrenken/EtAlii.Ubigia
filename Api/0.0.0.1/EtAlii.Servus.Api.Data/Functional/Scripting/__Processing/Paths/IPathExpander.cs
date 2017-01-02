namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;

    public interface IPathExpander
    {
        IEnumerable<string> Expand(Path path, out Identifier startIdentifier);
    }
}