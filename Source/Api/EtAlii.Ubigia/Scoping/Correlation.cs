// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

/// <summary>
/// This class provides a consistent list of all correlation ID's used all over the Ubigia code base.
/// </summary>
public static class Correlation
{
    public const string ScriptId = "ScriptCorrelationId";
    public const string UnitTestId = "UnitTestCorrelationId";

    public static readonly string[] AllIds = {ScriptId, UnitTestId};
}
