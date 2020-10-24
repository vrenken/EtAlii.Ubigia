namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting;

    public interface IServiceDetailsBuilder
    {
        ServiceDetails[] Build(IConfigurationDetails configurationDetails);
    }
}