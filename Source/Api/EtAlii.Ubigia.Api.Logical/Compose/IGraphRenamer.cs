// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public interface IGraphRenamer
    {
        Task<IReadOnlyEntry> Rename(Identifier item, string newName, ExecutionScope scope);
    }
}
