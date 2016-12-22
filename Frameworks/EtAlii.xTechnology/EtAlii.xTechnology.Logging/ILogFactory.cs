
namespace EtAlii.xTechnology.Logging
{
    public interface ILogFactory
    {
        ILogger Create(string name, string category);
    }
}
