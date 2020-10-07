
namespace EtAlii.xTechnology.Diagnostics
{
    public class DisabledLogFactory : ILogFactory
    {
        public ILogger Create(string name, string category)
        {
            return new DisabledLogger();
        }
    }
}
