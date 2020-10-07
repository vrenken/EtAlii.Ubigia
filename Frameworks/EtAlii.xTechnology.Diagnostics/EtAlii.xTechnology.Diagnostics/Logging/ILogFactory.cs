namespace EtAlii.xTechnology.Diagnostics
{
    public interface ILogFactory
    {
        ILogger Create(string name, string category);
    }
}
