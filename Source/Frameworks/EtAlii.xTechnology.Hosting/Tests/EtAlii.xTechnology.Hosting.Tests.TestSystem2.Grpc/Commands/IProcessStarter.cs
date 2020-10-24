namespace EtAlii.xTechnology.Hosting.Tests.Provisioning.Grpc
{

    public interface IProcessStarter
    {
        void StartProcess(string folder, string fileName, string arguments = "");
    }
}
