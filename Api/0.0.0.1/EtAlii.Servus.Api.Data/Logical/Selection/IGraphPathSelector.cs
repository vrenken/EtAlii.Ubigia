namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;

    public interface IGraphPathSelector
    {
        object Select(GraphPath path);
        IEnumerable<object> SelectMany(GraphPath path);
    }
}