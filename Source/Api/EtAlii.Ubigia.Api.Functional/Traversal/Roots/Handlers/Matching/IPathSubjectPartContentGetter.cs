﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System.Threading.Tasks;

public interface IPathSubjectPartContentGetter
{
    Task<string> GetPartContent(PathSubjectPart part, ExecutionScope scope);
}
