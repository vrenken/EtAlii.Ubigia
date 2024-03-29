﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System.Threading.Tasks;

internal class FragmentExecutionPlan<TFragment> : ExecutionPlan
    where TFragment: Fragment
{
    private readonly IFragmentProcessor<TFragment> _processor;
    private readonly TFragment _fragment;

    public FragmentExecutionPlan(
        TFragment fragment,
        IFragmentProcessor<TFragment> processor)
    {
        _processor = processor;
        _fragment = fragment;
    }

    internal override async Task Execute(ExecutionScope scope)
    {
        await _processor
            .Process(_fragment, ResultSink, scope)
            .ConfigureAwait(false);
    }

    public override string ToString()
    {
        return _fragment.ToString();
    }
}
