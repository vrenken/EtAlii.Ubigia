namespace EtAlii.Ubigia.Infrastructure.Transport.WebApi.Portal.Admin
{
    using System;
    using Microsoft.Owin;
    using Microsoft.Owin.FileSystems;
    using Microsoft.Owin.StaticFiles;
    using Owin;

    public partial class AdminPortalComponent : IAdminPortalComponent
    {

        public AdminPortalComponent()
        {
        }

        public void Start(IAppBuilder application)
        {
            application.UseFileServer(new FileServerOptions
            {
                DefaultFilesOptions = { DefaultFileNames = new[] { "Default.html" } },
                FileSystem = new EmbeddedResourceFileSystem("EtAlii.Ubigia.Infrastructure.Transport.WebApi.Portal.Admin"),
                RequestPath = new PathString("/Admin"),
                StaticFileOptions = { ServeUnknownFileTypes = true }
            });
        }

        public void Stop()
        {
            //throw new System.NotImplementedException();
        }

        public bool TryGetService(Type serviceType, out object serviceInstance)
        {
            serviceInstance = null;
            return false;
        }
    }
}