namespace EtAlii.xTechnology.Hosting
{
    using System.IO;
    using System.Text;
    using Microsoft.Extensions.Configuration;

    public static class NetworkDetailsConfigurationExtension
    {
        public static IConfigurationBuilder AddConfigurationDetails(this IConfigurationBuilder builder, ConfigurationDetails details)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(details.Configuration));
            return builder.AddJsonStream(stream);
        }
    }
}