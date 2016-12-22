namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Api;

    public static class IDataConnectionExtensions
    {
        public static void Open(this IDataConnection connection)
        {
            connection.Open("localhost", "local", "123");
        }
    }
}
