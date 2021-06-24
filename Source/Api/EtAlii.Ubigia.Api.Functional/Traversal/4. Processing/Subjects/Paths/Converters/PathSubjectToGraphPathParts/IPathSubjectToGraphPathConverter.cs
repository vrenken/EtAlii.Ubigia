// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    public interface IPathSubjectToGraphPathConverter
    {
        Task<GraphPath> Convert(PathSubject pathSubject, ExecutionScope scope);
    }
}
