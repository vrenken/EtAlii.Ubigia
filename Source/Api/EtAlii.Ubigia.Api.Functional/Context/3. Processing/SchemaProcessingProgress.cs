// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

public class SchemaProcessingProgress
{
    public Fragment Fragment { get; }
    public int Step { get; }
    public int Total { get; }

    public SchemaProcessingProgress(Fragment fragment, int step, int total)
    {
        Fragment = fragment;
        Step = step;
        Total = total;
    }
}
