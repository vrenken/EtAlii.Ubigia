namespace EtAlii.xTechnology.Hosting.Tests.Provisioning.NetCore
{

    public interface IProcessStarter
    {
        void StartProcess(string folder, string fileName, string arguments = "");
    }
}
