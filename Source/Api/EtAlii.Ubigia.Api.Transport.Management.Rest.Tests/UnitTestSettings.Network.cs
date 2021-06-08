namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using EtAlii.xTechnology.Hosting;

    public static class UnitTestSettings
    {
        public static readonly PortRange NetworkPortRange = new(UnitTestConstants.NetworkPortRangeStart + 14200, UnitTestConstants.NetworkPortRangeStart + 14399);
    }
}
