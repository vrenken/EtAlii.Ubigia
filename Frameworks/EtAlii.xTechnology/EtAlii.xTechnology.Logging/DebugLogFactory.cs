
namespace EtAlii.xTechnology.Logging
{
    public class DebugLogFactory : ILogFactory
    {
        public ILogger Create(string name, string category)
        {
            return new DebugLogger();
        }
    }
}
