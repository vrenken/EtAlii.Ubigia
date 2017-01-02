namespace EtAlii.Servus.Api.Tests
{
    using System;
    using System.Diagnostics;

    [DebuggerStepThrough]
    static internal class TestIdentifier
    {
        public static Identifier Create()
        {
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            var random = new Random();
            var era = (ulong)random.Next(0, Int32.MaxValue);
            var period = (ulong)random.Next(0, Int32.MaxValue);
            var moment = (ulong)random.Next(0, Int32.MaxValue);
            return Identifier.Create(storage, account, space, era, period, moment);
        }
    }
}