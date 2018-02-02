namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin
{
    using System;
    using global::Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.FileSystems;
    using Microsoft.Owin.StaticFiles;

    public class AdminPortalComponent : IAdminPortalComponent
    {
        public void Start(IAppBuilder application)
        {
            application.UseFileServer(new FileServerOptions
            {
                DefaultFilesOptions = { DefaultFileNames = new[] { "Default.html" } },
                FileSystem = new EmbeddedResourceFileSystem("EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin"),
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