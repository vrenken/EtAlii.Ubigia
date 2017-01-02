namespace EtAlii.Servus.Api.Data
{

    using EtAlii.xTechnology.MicroContainer;
    using Remotion.Linq.Parsing.ExpressionTreeVisitors.Transformation;
    using Remotion.Linq.Parsing.Structure;
    using Remotion.Linq.Parsing.Structure.NodeTypeProviders;

    public interface IRootSet
    {
        Queryable<Root> Select(string name);
    }
}