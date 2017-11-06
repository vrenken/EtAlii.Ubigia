namespace EtAlii.Ubigia.Infrastructure.Hosting
{

    public interface IProcessStarter
    {
        void StartProcess(string fileName, string arguments = "");
    }
}
