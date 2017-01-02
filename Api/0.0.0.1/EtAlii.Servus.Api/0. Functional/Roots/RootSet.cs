namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.Servus.Api.Fabric;

    internal class RootSet : IRootSet
    {
        private readonly RootQueryProviderFactory _rootQueryProviderFactory;

        protected internal RootSet(RootQueryProviderFactory rootQueryProviderFactory)
        {
            _rootQueryProviderFactory = rootQueryProviderFactory;
        }

        public Queryable<Root> Select(string name)
        {
            var queryProvider = _rootQueryProviderFactory.Create();
            return new Queryable<Root>(queryProvider);
        }
    }
}