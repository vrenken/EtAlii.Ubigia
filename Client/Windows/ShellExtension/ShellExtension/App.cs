using System;

namespace EtAlii.Ubigia.Client.Windows.ShellExtension
{
    public class App : EtAlii.Ubigia.Client.Windows.Shared.App
    {
        public static new App Current => _current.Value;
        private static Lazy<App> _current = new Lazy<App>(GetApp);

        private App()
        {
        }

        private static App GetApp()
        {
            var app = new App();
            app.RegisterKnownTypes();
            //app.Container.Verify();
            return app;
        }

        protected override void RegisterKnownTypes()
        {
            base.RegisterKnownTypes();
        }
    }
}
