namespace EtAlii.Servus.Api.Data
{

    using EtAlii.xTechnology.MicroContainer;
    using Remotion.Linq.Parsing.ExpressionTreeVisitors.Transformation;
    using Remotion.Linq.Parsing.Structure;
    using Remotion.Linq.Parsing.Structure.NodeTypeProviders;

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