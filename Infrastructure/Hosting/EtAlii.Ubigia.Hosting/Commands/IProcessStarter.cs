namespace EtAlii.Ubigia.Infrastructure.Hosting.Owin
{

    public interface IProcessStarter
    {
        void StartProcess(string fileName, string arguments = "");
    }
}
