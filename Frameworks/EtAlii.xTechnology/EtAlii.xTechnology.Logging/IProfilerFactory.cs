
namespace EtAlii.xTechnology.Logging
{
    public interface IProfilerFactory
    {
        IProfiler Create(string name, string category);
    }
}
