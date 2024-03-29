﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System.Threading.Tasks;

internal interface IValueGetter
{
    Task<Value> Get(string valueName, ValueAnnotation annotation, ExecutionScope scope, Structure structure);
}
