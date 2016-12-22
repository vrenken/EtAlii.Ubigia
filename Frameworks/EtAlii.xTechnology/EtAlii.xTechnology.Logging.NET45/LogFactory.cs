
namespace EtAlii.xTechnology.Logging
{
    public class LogFactory : ILogFactory
    {
        public ILogger Create(string name, string category)
        {
            return new Logger(name, category);
        }
    }
}
