namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Grpc
{

    public interface IProcessStarter
    {
        void StartProcess(string folder, string fileName, string arguments = "");
    }
}
