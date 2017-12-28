namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{

    public interface IProcessStarter
    {
        void StartProcess(string folder, string fileName, string arguments = "");
    }
}
