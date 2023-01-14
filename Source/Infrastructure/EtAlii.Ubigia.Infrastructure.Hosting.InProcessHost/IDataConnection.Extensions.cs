namespace EtAlii.Ubigia.Infrastructure.Hosting.Local
{
    using EtAlii.Ubigia;

    public static class IDataConnectionExtensions
    {
        public static void Open(this IDataConnection connection)
        {
            connection.Open("localhost", "local", "123");
        }
    }
}
