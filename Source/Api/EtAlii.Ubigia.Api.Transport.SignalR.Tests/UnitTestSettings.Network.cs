// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using EtAlii.xTechnology.Hosting;

    public static class UnitTestSettings
    {
        public static readonly PortRange NetworkPortRange = new(UnitTestConstants.NetworkPortRangeStart + 14400, UnitTestConstants.NetworkPortRangeStart + 14599);
    }
}
