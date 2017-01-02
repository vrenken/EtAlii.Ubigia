namespace EtAlii.Servus.Infrastructure.WebApi
{
    using Microsoft.Owin;
    using Microsoft.Owin.FileSystems;
    using Microsoft.Owin.StaticFiles;
    using Owin;

    public partial class ComponentManager : IComponentManager
    {
        private void StartInfoPage(IAppBuilder application)
        {
            application.UseFileServer(new FileServerOptions
            {
                DefaultFilesOptions = { DefaultFileNames = new []{"Default.html"}},
                FileSystem = new EmbeddedResourceFileSystem("EtAlii.Servus.Infrastructure.WebApi.Assets"),
            });
        }
    }
}