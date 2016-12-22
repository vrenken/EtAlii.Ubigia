
namespace EtAlii.xTechnology.Logging
{
    public class DisabledLogFactory : ILogFactory
    {
        public ILogger Create(string name, string category)
        {
            return new DisabledLogger();
        }
    }
}
