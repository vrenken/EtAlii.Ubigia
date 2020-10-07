
namespace EtAlii.xTechnology.Diagnostics
{
    public interface IProfilerFactory
    {
        IProfiler Create(string name, string category);
    }
}
