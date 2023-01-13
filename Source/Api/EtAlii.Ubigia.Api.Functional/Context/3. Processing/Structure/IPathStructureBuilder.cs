// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Functional.Traversal;

internal interface IPathStructureBuilder
{
    Task Build(
        ExecutionScope scope,
        ExecutionPlanResultSink executionPlanResultSink,
        NodeAnnotation annotation,
        string structureName,
        Structure parent,
        PathSubject path);
}
