namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.Ubigia.Api;

    public static class IDataConnectionExtensions
    {
        public static void Open(this IDataConnection connection)
        {
            connection.Open("localhost", "local", "123");
        }
    }
}
