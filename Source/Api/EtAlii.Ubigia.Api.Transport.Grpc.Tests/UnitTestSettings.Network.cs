
// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using EtAlii.xTechnology.Hosting;

    public static class UnitTestSettings
    {
        public static readonly PortRange NetworkPortRange = new(UnitTestConstants.NetworkPortRangeStart + 13600, UnitTestConstants.NetworkPortRangeStart + 13799);
    }
}
