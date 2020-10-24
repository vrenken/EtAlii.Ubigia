namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.NetCore
{

    public interface IProcessStarter
    {
        void StartProcess(string folder, string fileName, string arguments = "");
    }
}
