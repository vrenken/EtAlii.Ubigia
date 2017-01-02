namespace EtAlii.Servus.Api.Tests
{
    using SimpleInjector;


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
            //if (_current == null)
            //{
                var app = new T();

                app.RegisterTypes();
                app.Container.Verify();

                _current = app;
            //}
            //else if (_current.GetType() != typeof(T))
            //{
            //    throw new InvalidOperationException("Application setup is done twice with a different configuration");
            //}
        }

        protected virtual void RegisterTypes()
        {
        }
    }
}
