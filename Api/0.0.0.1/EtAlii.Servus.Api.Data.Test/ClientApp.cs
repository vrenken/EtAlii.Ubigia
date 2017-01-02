namespace EtAlii.Servus.Api.Tests
{
    using SimpleInjector;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class ClientApp
    {
        public static ClientApp Current { get { return _current; } }
        private static ClientApp _current;

        public readonly Container Container;

        public ClientApp()
        {
            Container = new Container();
        }

        public static void Setup<T>()
            where T : ClientApp, new()
        {
            var app = new T();

            app.RegisterTypes();
            app.Container.Verify();

            _current = app;
        }

        protected virtual void RegisterTypes()
        {
        }
    }
}
