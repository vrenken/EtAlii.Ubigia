namespace EtAlii.Ubigia.Infrastructure.Hosting.Owin
{

    public interface IProcessStarter
    {
        void StartProcess(string folder, string fileName, string arguments = "");
    }
}
