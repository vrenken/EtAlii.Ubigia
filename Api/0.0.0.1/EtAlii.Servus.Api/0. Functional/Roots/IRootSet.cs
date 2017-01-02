namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.Servus.Api.Fabric;

    public interface IRootSet
    {
        Queryable<Root> Select(string name);
    }
}