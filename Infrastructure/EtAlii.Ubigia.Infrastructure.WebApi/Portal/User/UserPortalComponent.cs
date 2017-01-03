namespace EtAlii.Ubigia.Infrastructure.WebApi.Portal.User
{
    using Microsoft.Owin.FileSystems;
    using Microsoft.Owin.StaticFiles;
    using Owin;

    public partial class UserPortalComponent : IUserPortalComponent
    {
        public UserPortalComponent()
        {
        }

        public void Start(IAppBuilder application)
        {
            application.UseFileServer(new FileServerOptions
            {
                DefaultFilesOptions = { DefaultFileNames = new[] { "Default.html" } },
                FileSystem = new EmbeddedResourceFileSystem("EtAlii.Ubigia.Infrastructure.WebApi.Portal.User"),
                //RequestPath = new PathString("/User"),
                StaticFileOptions = { ServeUnknownFileTypes = true }
            });
        }

        public void Stop()
        {
            //throw new System.NotImplementedException();
        }
    }
}