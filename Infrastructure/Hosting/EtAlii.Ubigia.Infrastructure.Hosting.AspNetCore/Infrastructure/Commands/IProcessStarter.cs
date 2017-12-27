namespace EtAlii.Ubigia.Infrastructure.Hosting.AspNetCore
{

    public interface IProcessStarter
    {
        void StartProcess(string folder, string fileName, string arguments = "");
    }
}
