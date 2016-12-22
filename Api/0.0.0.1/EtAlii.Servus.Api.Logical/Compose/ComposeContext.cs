namespace EtAlii.Servus.Api.Logical
{
    using EtAlii.Servus.Api.Fabric;

    public class ComposeContext : IComposeContext
    {
        public IFabricContext Fabric { get { return _fabric; } }
        private readonly IFabricContext _fabric;

        public ComposeContext(IFabricContext fabric)
        {
            _fabric = fabric;
        }
    }
}
