﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

public class ScriptProcessingProgress
{
    public Sequence Sequence { get; }
    public int Step { get; }
    public int Total { get; }

    public ScriptProcessingProgress(Sequence sequence, int step, int total)
    {
        Sequence = sequence;
        Step = step;
        Total = total;
    }
}
