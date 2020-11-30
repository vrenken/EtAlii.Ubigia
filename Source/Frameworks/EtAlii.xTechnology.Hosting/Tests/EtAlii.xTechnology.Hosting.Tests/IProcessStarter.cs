namespace EtAlii.xTechnology.Hosting.Tests
{

    public interface IProcessStarter
    {
        void StartProcess(string folder, string fileName, string arguments = "");
    }
}
