// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal interface INodeValidator
    {
        void EnsureSuccess(LpNode node, string requiredId, bool restIsAllowed = true);
    }
}
