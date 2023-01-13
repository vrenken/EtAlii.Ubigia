// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System.Threading.Tasks;

internal interface IFragmentProcessor<in TFragment>
    where TFragment: Fragment
{
    Task Process(
        TFragment fragment,
        ExecutionPlanResultSink executionPlanResultSink,
        ExecutionScope scope);

}
