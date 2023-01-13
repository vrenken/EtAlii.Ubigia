namespace EtAlii.Ubigia.Tests;

using System;
using System.Diagnostics;

[DebuggerStepThrough]
public class TestIdentifierFactory
{
    private readonly Random _random;

    public TestIdentifierFactory()
    {
        _random = new Random(1234567890);
    }
    public Identifier Create()
    {
        var storage = Guid.NewGuid();
        var account = Guid.NewGuid();
        var space = Guid.NewGuid();
        var era = (ulong)_random.Next(0, int.MaxValue);
        var period = (ulong)_random.Next(0, int.MaxValue);
        var moment = (ulong)_random.Next(0, int.MaxValue);
        return Identifier.Create(storage, account, space, era, period, moment);
    }
}
