namespace EtAlii.Servus.Api.Logical
{
    using System.Collections.Generic;

    public interface IGraphPathSelector
    {
        object Select(GraphPath path);
        IEnumerable<object> SelectMany(GraphPath path);
    }
}