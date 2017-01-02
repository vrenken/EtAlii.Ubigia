namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GraphPathSelector : IGraphPathSelector
    {
        public object Select(GraphPath path)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> SelectMany(GraphPath path)
        {
            throw new NotImplementedException();
        }
    }
}
