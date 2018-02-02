namespace EtAlii.Ubigia.Infrastructure.Transport
{

    public interface IProcessStarter
    {
        void StartProcess(string folder, string fileName, string arguments = "");
    }
}
