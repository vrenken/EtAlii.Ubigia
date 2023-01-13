// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System.Threading.Tasks;

public interface ITraversalContextRootSet
{
    Task<Root> Get(string name, ExecutionScope scope);
}
