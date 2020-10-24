
namespace EtAlii.xTechnology.Diagnostics
{
    public class DisabledProfilerFactory : IProfilerFactory
    {
        public IProfiler Create(string name, string category)
        {
            return new DisabledProfiler();
        }
    }
}
