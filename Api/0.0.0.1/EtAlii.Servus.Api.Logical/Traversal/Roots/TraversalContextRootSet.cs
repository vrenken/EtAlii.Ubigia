namespace EtAlii.Servus.Api.Logical
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Fabric;

    public class TraversalContextRootSet : ITraversalContextRootSet
    {
        private readonly IFabricContext _fabricContext;

        private readonly IDictionary<string, Root> _cache; 

        public TraversalContextRootSet(IFabricContext fabricContext)
        {
            _fabricContext = fabricContext;
            _cache = new Dictionary<string, Root>();
        }

        public async Task<Root> Get(string rootName)
        {
            Root result;
            if (!_cache.TryGetValue(rootName, out result))
            {
                _cache[rootName] = result = await _fabricContext.Roots.Get(rootName);
            }
            return result;
        }
    }
}